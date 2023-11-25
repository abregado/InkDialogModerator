
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerHandler: MonoBehaviour {
    public SpeakerConfig[] speakers;

    private Dictionary<string, SpeakerConfig> _sortedConfigs;

    private void Awake() {
        _sortedConfigs = new Dictionary<string, SpeakerConfig>();
        
        foreach (var config in speakers) {
            _sortedConfigs.Add(config.identifier,config);    
        }
    }

    public SpeakerConfig GetConfig(string identifier) {
        return _sortedConfigs[identifier];
    }
}
