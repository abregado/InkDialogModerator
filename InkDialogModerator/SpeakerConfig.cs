using UnityEngine;


[CreateAssetMenu(fileName = "SpeakerConfig", menuName = "SpeakerConfig", order = 1)]
public class SpeakerConfig: ScriptableObject {
    public enum NamePlateAlignment {
        Left,
        Right
    }
    
    public string identifier = "default";
    public string englishDisplayName = "Default";

    public Sprite namePlateBackground;
    public Sprite dialogBackground;
    public Color borderColor;
    public Color textColor;
    public NamePlateAlignment namePlateAlignment = NamePlateAlignment.Left;
}
