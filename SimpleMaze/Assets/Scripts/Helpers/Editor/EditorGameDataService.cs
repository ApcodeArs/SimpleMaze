using GameCore.Services;
using UnityEditor;
using UnityEngine;

namespace Helpers.Editor {
    [CustomEditor(typeof(GameDataService))]
    public class EditorGameDataService: UnityEditor.Editor {
        private GameDataService _gameDataService;
        
        public void Awake() {
            _gameDataService = (GameDataService)target;
        }
        
        public override void OnInspectorGUI() {
            EditorGUILayout.Space();
            
            if (!Application.isPlaying && GUILayout.Button("Reset Data")) {
                _gameDataService.Reset();
            }
        }
    }
}