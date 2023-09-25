using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogPanel : MonoBehaviour {
    public TextMeshProUGUI dialogTextTarget;
    public TextMeshProUGUI speakerNameTextTarget;
    
    private void Awake() {
        if (!FindObjectOfType<InkDialogModerator>().tagsToAction.Contains("speaker")) {
            Debug.LogWarning("InkDialogModerator is not set to action speaker tags.");
        }
        Debug.Assert(dialogTextTarget!=null,"DialogPanel requires a dialogTextTarget.");
        Debug.Assert(speakerNameTextTarget!=null,"DialogPanel requires a speakerNameTextTarget.");
    }

    private void OnEnable() {
        ModeratorEvents.OnDialogChange += DisplayDialogText;
        ModeratorEvents.OnTagInvoked += ChangeSpeakerName;
    }

    private void OnDisable() {
        ModeratorEvents.OnDialogChange -= DisplayDialogText;
        ModeratorEvents.OnTagInvoked -= ChangeSpeakerName;
    }

    private void DisplayDialogText(string text) {
        dialogTextTarget.text = text;
    }

    private void ChangeSpeakerName(string tag, string[] param) {
        if (tag == "speaker" && param.Length > 0) {
            speakerNameTextTarget.text = param[0];
        }
    }
}
