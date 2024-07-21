using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "SpeedMultipliers", menuName = "ScriptableObjects/SpeedMultipliers", order = 3)]
    public class SpeedMultipliers: ScriptableObject {
        [SerializeField] private float _keyboard;
        [SerializeField] private float _accelerometer;
        
        public float Keyboard => _keyboard;
        public float Accelerometer => _accelerometer;
    }
}