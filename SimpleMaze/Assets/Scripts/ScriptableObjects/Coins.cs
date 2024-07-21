using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "Coins", menuName = "ScriptableObjects/Coins", order = 2)]
    public class Coins: ScriptableObject {
        [SerializeField][Range(0, 1)]
        private float _mazeMinCoverage;
        [SerializeField][Range(0, 1)]
        private float _mazeMaxCoverage;
        
        public float MazeMinCoverage => _mazeMinCoverage;
        public float MazeMaxCoverage => _mazeMaxCoverage;
    }
}