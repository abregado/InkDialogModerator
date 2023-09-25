
using System;
using UnityEngine;

public class ContinueControllerUnityInput: MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ModeratorEvents.TriggerOnRequestDialogContinue();
        }
    }
}
