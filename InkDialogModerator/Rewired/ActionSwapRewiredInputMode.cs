using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class ActionSwapRewiredInputMode: MonoBehaviour {
    public bool swapSystemPlayer = false;
    public string[] gameplayMaps;
    public string[] cutsceneMaps;

    private void OnEnable() {
        ModeratorEvents.OnCutsceneBegins += OnCutsceneBegins;
        ModeratorEvents.OnCutsceneEnds += OnCutsceneEnds;
    }

    private void OnDisable() {
        ModeratorEvents.OnCutsceneBegins -= OnCutsceneBegins;
        ModeratorEvents.OnCutsceneEnds -= OnCutsceneEnds;
    }

    public void OnCutsceneBegins() {
        SwitchCutsceneInputMode(true);
    }

    public void OnCutsceneEnds() {
        SwitchCutsceneInputMode(false);
    }
    
    private void SwitchCutsceneInputMode(bool cutsceneMode) {
        IList<Player> players;
        
        if (swapSystemPlayer) {
            players = ReInput.players.AllPlayers;
        }
        else {
            players = ReInput.players.Players;
        }
        
        foreach (Player player in players) {
            if (player.isPlaying) {
                Debug.Log("Player " + player.id +" swapped to Gameplay mode");
                foreach (string mapName in gameplayMaps) {
                    player.controllers.maps.SetMapsEnabled(!cutsceneMode, mapName);    
                }
                foreach (string mapName in cutsceneMaps) {
                    player.controllers.maps.SetMapsEnabled(cutsceneMode, mapName);    
                }    
            }
        }
    }
}
