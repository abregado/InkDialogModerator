using DG.Tweening;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class CutscenePanelUITK: MonoBehaviour {
    private UIDocument _document;
    private VisualElement _root;

    private Tween _dialogTween;
    
    [Tooltip("Measured in characters per second. Average human reading speed is 25cps.")]
    public float printSpeed = 25f;

    public float fastModeSpeedMod = 50f;
    
    [Header("Look")]
    public Sprite buttonBackground;
    public Sprite speakerBackground;
    public Sprite dialogBackground;

    private void Awake() {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;
    }

    private void OnEnable() {
        ModeratorEvents.OnCutsceneBegins += OnCutsceneBegins;
        ModeratorEvents.OnDialogChange += OnDialogChange;
        ModeratorEvents.OnCutsceneEnds += OnCutsceneEnds;
        ModeratorEvents.OnChoiceChange += OnChoiceChange;
        ModeratorEvents.OnTagInvoked += OnTagInvoked;
        ModeratorEvents.OnRequestDialogContinue += OnRequestDialogContinue;
    }

    private void OnDisable() {
        ModeratorEvents.OnCutsceneBegins -= OnCutsceneBegins;
        ModeratorEvents.OnDialogChange -= OnDialogChange;
        ModeratorEvents.OnCutsceneEnds -= OnCutsceneEnds;
        ModeratorEvents.OnChoiceChange -= OnChoiceChange;
        ModeratorEvents.OnTagInvoked -= OnTagInvoked;
        ModeratorEvents.OnRequestDialogContinue -= OnRequestDialogContinue;
    }

    private void OnTagInvoked(string tag, string[] parameters) {
        if (tag == "speaker") {
            SpeakerHandler handler = FindObjectOfType<SpeakerHandler>();
            SpeakerConfig config = handler.GetConfig(parameters[0]);
            if (config == null) return;

            Label namePlate = _root.Q<Label>("speaker-name");
            namePlate.text = config.englishDisplayName;
            namePlate.style.backgroundImage = new StyleBackground(config.namePlateBackground);
            namePlate.style.color = config.textColor;
            namePlate.style.unityBackgroundImageTintColor = config.borderColor;
            
            Label dialog = _root.Q<Label>("dialog-text");
            dialog.style.backgroundImage = new StyleBackground(config.dialogBackground);
            dialog.style.color = config.textColor;
            dialog.style.unityBackgroundImageTintColor = config.borderColor;

            if (config.namePlateAlignment == SpeakerConfig.NamePlateAlignment.Left) {
                namePlate.RemoveFromClassList("right");
                namePlate.AddToClassList("left");
            }
            else {
                namePlate.RemoveFromClassList("left");
                namePlate.AddToClassList("right");
            }

        }
    }

    private void OnCutsceneBegins() {
        _document.enabled = true;
        Build();
    }

    private void OnCutsceneEnds() {
        _document.enabled = false;
    }

    private void OnRequestDialogContinue() {
        if (_dialogTween == null) {
            return;
        }
        
        if (_dialogTween!=null && _dialogTween.IsPlaying()) {
            _dialogTween.timeScale = fastModeSpeedMod;
        }
    }

    private void OnDialogChange(string newDialog) {
        Label dialog = _root.Q<Label>("dialog-text");
        dialog.text = "";
        _dialogTween = DOTween.To(() => dialog.text,
            x => dialog.text = x, newDialog,
            newDialog.Length/printSpeed);
        _dialogTween.OnComplete(() => { ModeratorEvents.TriggerOnRequestStoryResume(); });
        _dialogTween.SetAutoKill(true);
        _dialogTween.Play();
        ModeratorEvents.TriggerOnRequestStoryPause();
    }

    private void OnChoiceChange(Choice[] choices) {
        BuildChoiceList(choices);
    }

    private void Build() {
        _root.Clear();

        //left to right, bottom aligned
        VisualElement container = UIFactory.GenerateElement(
            "cutscene-panel", 
            new []{"cutscene","container"}
            );
        
        VisualElement leftPadding = UIFactory.GenerateElement(
            "left-panel", 
            new []{"side-panel"}
            );
        
        VisualElement dialogPanel = UIFactory.GenerateElement(
            "dialog-panel", 
            new []{"center-panel","dialog-panel"}
            );
        
        VisualElement choicePanel = UIFactory.GenerateElement(
            "choice-panel", 
            new []{"side-panel","choice-panel"}
            );
        
        container.Add(leftPadding);
        container.Add(dialogPanel);
        container.Add(choicePanel);

        Label dialogText = UIFactory.GenerateLabel(
            "dialog-text",
            "Default dialog text",
            new[] {"dialog-text"}
        );
        dialogText.style.backgroundImage = new StyleBackground(dialogBackground);

        Label speakerName = UIFactory.GenerateLabel(
            "speaker-name", 
            "Speaker", 
            new[] {"speaker-name"}
        );
        speakerName.style.backgroundImage = new StyleBackground(speakerBackground);
        
        dialogPanel.Add(speakerName);
        dialogPanel.Add(dialogText);
        
        _root.Add(container);
    }

    private void BuildChoiceList(Choice[] choices) {
        VisualElement choicePanel = _root.Q<VisualElement>("choice-panel");
        
        foreach (var choice in choices) {
            Button choiceBut = UIFactory.GenerateButton(
                "choice-" + choices[0].index,
                choice.text
            );
            choiceBut.clicked += delegate {
                ModeratorEvents.TriggerOnChoiceMade(choice);
                choicePanel.Clear();
            };
            choiceBut.AddToClassList("choice-button");
            choiceBut.style.backgroundImage = new StyleBackground(buttonBackground);
            choiceBut.RemoveFromClassList("unity-button");
            choiceBut.RemoveFromClassList("unity-text-element");
            choicePanel.Add(choiceBut);
        }
    }
}
