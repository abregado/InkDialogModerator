using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class ChoicePanel : MonoBehaviour {
    public GameObject choiceButtonPrefab;
    public RectTransform contentContainer;

    private void Awake() {
        Debug.Assert(contentContainer != null, "ChoicePanel requires a contentContainer target.");
    }

    private void OnEnable() {
        ModeratorEvents.OnChoiceChange += ShowChoices;
        ModeratorEvents.OnCutsceneBegins += HideCutscenePanel;
    }

    private void OnDisable() {
        ModeratorEvents.OnChoiceChange -= ShowChoices;
        ModeratorEvents.OnCutsceneBegins -= HideCutscenePanel;
    }

    public void MakeChoice(Choice choice) {
        ModeratorEvents.TriggerOnChoiceMade(choice);
        DestroyChildren();
        HideCutscenePanel();
    }
    
    private void ShowCutscenePanel() {
        GetComponent<CanvasGroup>().alpha = 1f;
    }

    private void HideCutscenePanel() {
        GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void DestroyChildren() {
        for (int i = contentContainer.transform.childCount - 1; i >= 0; i--) {
            Destroy(contentContainer.transform.GetChild(i).gameObject);
        }
    }

    public void ShowChoices(Choice[] choices) {
        ShowCutscenePanel();
        List<GameObject> buttonList = new List<GameObject>();

        for (int i = 0; i < choices.Length; i++) {
            Choice choice = choices[i];
            GameObject temp = Instantiate(choiceButtonPrefab, contentContainer.transform);
            ChoiceButton script = temp.GetComponent<ChoiceButton>();
            script.Init(choice, this);
            buttonList.Add(temp);
        }

        if (choices.Length > 1) {
            Button lastButton = buttonList[buttonList.Count - 1].GetComponent<Button>();
            Button firstButton = buttonList[0].GetComponent<Button>();

            for (int i = 0; i < choices.Length; i++) {
                Button thisButton = buttonList[i].GetComponent<Button>();
                Navigation thisNav = thisButton.navigation;

                Button prevButton = null;
                Button nextButton = null;

                if (i > 0) {
                    prevButton = buttonList[i - 1]?.GetComponent<Button>();
                }

                if (i < choices.Length - 1) {
                    nextButton = buttonList[i + 1]?.GetComponent<Button>();
                }

                if (prevButton != null) {
                    thisNav.selectOnUp = prevButton;
                }

                if (nextButton != null) {
                    thisNav.selectOnDown = nextButton;
                }

                if (nextButton == null) {
                    thisNav.selectOnDown = firstButton;
                }

                if (prevButton == null) {
                    thisNav.selectOnUp = lastButton;
                }

                thisButton.navigation = thisNav;
            }

            SelectBottomOption();
        }
    }
    
    private void SelectBottomOption() {
        EventSystem system = FindObjectOfType<EventSystem>();
        GameObject last = contentContainer.transform.GetChild(contentContainer.transform.childCount - 1).gameObject;
        system.SetSelectedGameObject(last);
    }

}
