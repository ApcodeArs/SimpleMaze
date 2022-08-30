using Controllers;
using UnityEditor;
using UnityEngine;

namespace Helpers.Editor {
    [CustomEditor(typeof(MazeController))]
    public class EditorMazeController: UnityEditor.Editor {
        private MazeController _mazeController;
        
        public void Awake() {
            _mazeController = (MazeController)target;
        }

        public override void OnInspectorGUI() {
            EditorGUILayout.Space();
            
            if (Application.isPlaying && GUILayout.Button("Refresh")) {
                _mazeController.Init();
            }
        }
    }
}