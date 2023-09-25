using UnityEngine;

public class ActorAction : MonoBehaviour
{
    public string triggerTag = "default";
    
    private void OnEnable() {
        ModeratorEvents.OnTagInvoked += ProcessTag;
    }

    private void OnDisable() {
        ModeratorEvents.OnTagInvoked -= ProcessTag;
    }

    protected virtual void ProcessTag(string tagName, string[] param) {
        if (tagName == triggerTag) {
            Debug.Log("Tag is for " + gameObject.name);
        }        
    }
}
