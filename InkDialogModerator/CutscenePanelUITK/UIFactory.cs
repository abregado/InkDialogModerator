using System.Collections.Generic;
using UnityEngine.UIElements;

public class UIFactory {
    
    public delegate void ClickEventDelegate(ClickEvent evt);
    
    public static VisualElement GenerateElement(string name, string[] classList = null) {
        classList = classList ?? new string[] {};
        VisualElement element = new VisualElement();
        element.name = name;
        foreach (var style in classList) {
            element.AddToClassList(style);
        }
        return element;
    }
    
    public static Button GenerateButton(string name, string text) {
        Button button = new Button();
        button.name = name;
        button.text = text;
        //button.RegisterCallback<ClickEvent>(action.Invoke);
        return button;
    }
    
    public static Button GenerateButtonFromTemplate(VisualTreeAsset template, string name, string text, ClickEventDelegate action) {
        TemplateContainer tc = template.Instantiate();
        Button button = tc.Q<Button>("button");
        button.name = name;
        button.text = text;
        button.RegisterCallback<ClickEvent>(action.Invoke);
        return button;
    }

    public static TextField GenerateTextfieldFromTemplate(VisualTreeAsset template, string name, string label, string text) {
        TemplateContainer tc = template.Instantiate();
        TextField textField = tc.Q<TextField>("textfield");
        textField.name = name;
        textField.label = label;
        textField.value = text;
        return textField;
    }
    
    

    public static Label GenerateLabel(string name, string text, string[] classList = null) {
        classList = classList ?? new string[] {};
        Label label = new Label();
        label.name = name;
        label.text = text;
        foreach (var style in classList) {
            label.AddToClassList(style);
        }
        return label;
    }
}