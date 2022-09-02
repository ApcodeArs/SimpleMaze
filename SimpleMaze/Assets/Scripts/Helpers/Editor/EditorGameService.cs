using GameCore.Services;
using UnityEditor;
using UnityEngine;

namespace Helpers.Editor {
    [CustomEditor(typeof(GameService))]
    public class EditorGameService: UnityEditor.Editor {
        private GameService _gameService;
        
        public void Awake() {
            _gameService = (GameService)target;
        }

        public override void OnInspectorGUI() {
            EditorGUILayout.Space();
            
            if (Application.isPlaying && GUILayout.Button("Refresh")) {
                _gameService.LoadLevel();
            }
        }
    }
}