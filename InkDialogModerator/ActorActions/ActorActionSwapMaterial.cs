using System;
using UnityEngine;

public class ActorActionSwapMaterial : ActorAction {
    public string actorName = "default";
    public Material swapMaterial;
    private Material _oldMaterial;

    private void Awake() {
        Renderer rend = GetComponent<Renderer>();
        _oldMaterial = rend.material;
    }

    protected override void ProcessTag(string tagName, string[] param) {
        if (tagName == triggerTag && param.Length > 0 && param[0] == actorName) {
            Renderer rend = GetComponent<Renderer>();
            if (rend.material == _oldMaterial) {
                rend.material = swapMaterial;
            }
            else {
                rend.material = _oldMaterial;
            }
        }
    }
}
