using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "Points", menuName = "ScriptableObjects/Points", order = 1)]
    public class Points: ScriptableObject {
        [SerializeField] private int _levelComplete;
        [SerializeField] private int _coinCollect;
        
        public int LevelComplete => _levelComplete;
        public int CoinCollect=> _coinCollect;
    }
}