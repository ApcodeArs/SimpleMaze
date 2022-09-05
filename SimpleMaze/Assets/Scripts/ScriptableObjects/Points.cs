using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "Points", menuName = "ScriptableObjects/Points", order = 1)]
    public class Points: ScriptableObject {
        [SerializeField] private int _levelComplete;
        
        public int LevelComplete => _levelComplete;
    }
}