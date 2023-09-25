using Ink.Runtime;

public static class ModeratorEvents {
    public delegate void CutsceneBegins();
    public static event CutsceneBegins OnCutsceneBegins;
    public static void TriggerOnCutsceneBegins() {
        OnCutsceneBegins?.Invoke();
    }
    
    public delegate void CutsceneEnds();
    public static event CutsceneEnds OnCutsceneEnds;
    public static void TriggerOnCutsceneEnds() {
        OnCutsceneEnds?.Invoke();
    }
    
    public delegate void DialogChange(string newDialog);
    public static event DialogChange OnDialogChange;
    public static void TriggerOnDialogChange(string newDialog) {
        OnDialogChange?.Invoke(newDialog);
    }
    
    public delegate void ChoiceChange(Choice[] newChoices);
    public static event ChoiceChange OnChoiceChange;
    public static void TriggerOnChoiceChange(Choice[] newChoices) {
        OnChoiceChange?.Invoke(newChoices);
    }
    
    public delegate void ChoiceMade(Choice selected);
    public static event ChoiceMade OnChoiceMade;
    public static void TriggerOnChoiceMade(Choice selected) {
        OnChoiceMade?.Invoke(selected);
    }
    
    public delegate void RequestDialogContinue();
    public static event RequestDialogContinue OnRequestDialogContinue;
    public static void TriggerOnRequestDialogContinue() {
        OnRequestDialogContinue?.Invoke();
    }


    public delegate void RequestCutscene(int storyIndex, string startKnot);
    public static event RequestCutscene OnRequestCutscene;
    public static void TriggerOnRequestCutscene(int storyIndex, string startKnot) {
        OnRequestCutscene?.Invoke(storyIndex, startKnot);
    }
    public static void TriggerOnRequestCutscene(int storyIndex) {
        OnRequestCutscene?.Invoke(storyIndex, "");
    }
    
    public delegate void TagInvoked(string tag, string[] parameters);
    public static event TagInvoked OnTagInvoked;
    public static void TriggerOnTagInvoked(string tag, string[] parameters) {
        OnTagInvoked?.Invoke(tag, parameters);
    }
    
}
