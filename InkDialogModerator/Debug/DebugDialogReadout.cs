using System;
using UnityEngine;

public class DebugDialogReadout : MonoBehaviour
{
    private void OnEnable() {
        ModeratorEvents.OnDialogChange += PrintDialog;
    }

    private void OnDisable() {
        ModeratorEvents.OnDialogChange -= PrintDialog;
    }

    private void PrintDialog(string newDialog) {
        Debug.Log(newDialog);
    }
}
