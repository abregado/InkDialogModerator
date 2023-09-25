using System.Linq;
using DefaultNamespace;
using Ink.Runtime;
using UnityEngine;

public class InkDialogModerator : MonoBehaviour
{
    [Header("Story Starting State")]
    public TextAsset[] inkFiles;
    public string defaultStartKnot;
    public int sceneStartsInDialog = -1;
    
    [Header("Hookups")]
    public string[] tagsToAction;
    
    private StoryScribe _scribe;

    private Story _story;

    void Awake() {
        _scribe = new StoryScribe();
    }

    private void OnEnable() {
        ModeratorEvents.OnRequestCutscene += BeginStory;
        ModeratorEvents.OnChoiceMade += ChoiceMadeCallback;
        ModeratorEvents.OnRequestDialogContinue += ContinueCallback;
    }

    private void OnDisable() {
        ModeratorEvents.OnRequestCutscene -= BeginStory;
        ModeratorEvents.OnChoiceMade -= ChoiceMadeCallback;
        ModeratorEvents.OnRequestDialogContinue -= ContinueCallback;
    }

    private void Start() {
        if (sceneStartsInDialog > -1) {
            BeginStory(sceneStartsInDialog,defaultStartKnot);
        }
    }

    public void BeginStory(int storyNumber, string startKnot = "") {
        if (_story != null) {
            Debug.LogWarning("Trying to trigger a cutscene to start, but one is already running.");
            return;
        }
        
        if (inkFiles.Length <= storyNumber) {
            Debug.LogError("DialogManager: Couldn't find story with index of " + storyNumber);
            return;
        }
        
        _story = new Story(inkFiles[storyNumber].text);
        
        LoadVariablesFromScribe();
        
        ModeratorEvents.TriggerOnCutsceneBegins();
        
        if (startKnot != "") {
            JumpToKnot(startKnot);
        }
        else {
            AdvanceDialogue();
        }
        
    }
    
    public void JumpToKnot(string pathname) {
        _story.ChoosePathString(pathname);
        AdvanceDialogue();
    }
    
    private void AdvanceDialogue() {
        string currentSentence = _story.Continue();
        ParseTags();
        if (_story.currentChoices.Count != 0) {
            ModeratorEvents.TriggerOnChoiceChange(_story.currentChoices.ToArray());
        }
        ModeratorEvents.TriggerOnDialogChange(currentSentence);
    }
    
    public void ContinueCallback() {
        if (_story.canContinue) {
            AdvanceDialogue();
            return;
        }
        if (_story.currentChoices.Count == 0) {
            FinishDialogue();
        }
    }

    public void ChoiceMadeCallback(Choice selected) {
        _story.ChooseChoiceIndex(selected.index);
        AdvanceDialogue();
    }

    private void ParseTags() {
        foreach (string t in _story.currentTags) {
            string prefix = t.Split(' ').ElementAtOrDefault(0);
            var param = t.Split(' ').Where(x=> x != prefix).ToArray();
            //check for tag in the actions list
            if (tagsToAction.Contains(prefix)) {
                ModeratorEvents.TriggerOnTagInvoked(prefix,param);    
            }
        }
    }

    private void FinishDialogue() {
        SaveStoryVariables();
        _story = null;
        ModeratorEvents.TriggerOnCutsceneEnds();
        
        //CheckStoryVariableTriggers();
        //_manager.levelManager.CheckLookTargets();
        //send event DialogClosing
        // _manager.playerManager.SwitchToInGameMode();
        // _manager.playerManager.SetLocalPlayerControllability(true);
        // SwapCamera("gameplay");
        // _optionsPanel.DestroyChildren();
    }

    private void LoadVariablesFromScribe() {
        _story = _scribe.Load(_story);
    }
    
    private void SaveStoryVariables() {
        _scribe.Save(_story);
    }
}
 
