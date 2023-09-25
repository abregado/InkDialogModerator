using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CutscenePanel : MonoBehaviour
{
    private void OnEnable() {
        ModeratorEvents.OnCutsceneBegins += ShowCutscenePanel;
        ModeratorEvents.OnCutsceneEnds += HideCutscenePanel;
    }

    private void OnDisable() {
        ModeratorEvents.OnCutsceneBegins -= ShowCutscenePanel;
        ModeratorEvents.OnCutsceneEnds -= HideCutscenePanel;
    }

    private void ShowCutscenePanel() {
        GetComponent<CanvasGroup>().alpha = 1f;
    }

    private void HideCutscenePanel() {
        GetComponent<CanvasGroup>().alpha = 0f;
    }
}
