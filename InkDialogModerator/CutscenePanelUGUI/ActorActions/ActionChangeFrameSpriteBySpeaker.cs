using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionChangeFrameSpriteBySpeaker: ActorAction {
    public SpeakerSpritePair[] speakerFrames;
    public Image frameTarget;

    protected override void ProcessTag(string tagName, string[] param) {
        if (tagName == triggerTag && param.Length > 0) {
            Debug.Log("Getting frame for " + param[0]);
            SpeakerSpritePair speakerFrame = GetFrameForSpeaker(param[0]);
            if (speakerFrame != null) {
                frameTarget.sprite = speakerFrame.frameSprite;
                frameTarget.color = speakerFrame.color;
            }
        }
    }

    private SpeakerSpritePair GetFrameForSpeaker(string speaker) {
        foreach (var pair in speakerFrames) {
            if (pair.speakerName == speaker) {
                return pair;
            }
        }

        return null;
    }
}

[Serializable]
public class SpeakerSpritePair {
    public string speakerName;
    public Sprite frameSprite;
    public Color color;
}
