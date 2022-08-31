using Controllers;
using UnityEditor;
using UnityEngine;

namespace Helpers.Editor {
    [CustomEditor(typeof(GameController))]
    public class EditorGameController: UnityEditor.Editor {
        private GameController _gameController;
        
        public void Awake() {
            _gameController = (GameController)target;
        }

        public override void OnInspectorGUI() {
            EditorGUILayout.Space();
            
            if (Application.isPlaying && GUILayout.Button("Refresh")) {
                _gameController.LoadLevel();
            }
        }
    }
}