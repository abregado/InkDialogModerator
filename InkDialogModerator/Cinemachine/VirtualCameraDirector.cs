using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirtualCameraDirector : ActorAction {
    private VirtualCameraOperator[] _cameras;
    
    private void Awake() {
        if (!FindObjectOfType<InkDialogModerator>().tagsToAction.Contains("camera")) {
            Debug.LogWarning("InkDialogModerator is not set to action camera tags.");
        }
        _cameras = FindObjectsOfType<VirtualCameraOperator>().ToArray();
    }

    protected override void ProcessTag(string tagName, string[] param) {
        if (tagName == triggerTag && param.Length > 0) {
            SwapCamera(param[0]);
        }
    }

    public void SwapCamera(string cameraName) {
        Debug.Log("Swapping to camera named " + cameraName);
        
        foreach (var cameraOperator in _cameras) {
            if (cameraOperator.operatorName == cameraName) {
                cameraOperator.MakePrimary();
                return;
            }
            cameraOperator.MakeSecondary();    
        }
    }
}
