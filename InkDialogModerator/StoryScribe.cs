using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

namespace DefaultNamespace {
    public class StoryScribe {
        
        private Dictionary<string, bool> _variableCache = new ();
        private Dictionary<string, int> _visitCache = new ();
        
        public Story Load(Story story) {
            foreach (var pair in _variableCache) {
                if (story.variablesState.GetVariableWithName(pair.Key)) {
                    story.variablesState.SetGlobal(pair.Key,new Ink.Runtime.BoolValue(pair.Value));
                }
            }

            foreach (var pair in _visitCache) {
                var container = story.ContentAtPath(new Path(pair.Key)).container;
                if (container != null && pair.Value > 0) {
                    story.state.IncrementVisitCountForContainer(container);
                }
            }

            return story;
        }

        public void Save(Story story) {
            foreach (string key in story.variablesState) {
                bool value = ConvertInkVariable(story.variablesState.GetVariableWithName(key));
                SetCacheVariable(key,value);
            }
        
            foreach (var pair in story.mainContentContainer.namedContent) {
                int visits = story.state.VisitCountAtPathString(pair.Key);
                SetVisit(pair.Key,visits);
            }
        }
        
        public void SetCacheVariable(string key, bool value) {
            if (!_variableCache.ContainsKey(key)) {
                _variableCache.Add(key,value);
                return;
            }
            _variableCache[key] = value;
        }

        private void SetVisit(string key, int visits) {
            if (!_visitCache.ContainsKey(key)) {
                _visitCache.Add(key,visits);
                return;
            }
            _visitCache[key] = visits;
        }
        
        private bool ConvertInkVariable(Ink.Runtime.Object variable) {
            string stringVar = variable.ToString();
            if (stringVar == "false") {
                return false;
            }
            if (stringVar == "true") {
                return true;
            }
            if (stringVar == "0") {
                return false;
            }
            if (stringVar == "1") {
                return true;
            }

            return false;
        }
        
        public bool CheckVariableExists(string variableName) {
            if (_variableCache.ContainsKey(variableName)) {
                return _variableCache[variableName];
            }

            return false;
        }
    }
}