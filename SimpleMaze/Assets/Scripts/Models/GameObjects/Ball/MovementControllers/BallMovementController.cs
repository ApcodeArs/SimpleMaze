using System;
using ScriptableObjects;
using UnityEngine;

namespace Models.GameObjects.Ball.MovementControllers {
    public class BallMovementController {
        private BaseMovementController _movementController;

        public BallMovementController(GameObject target, SpeedMultipliers speedMultipliers) {
            Init(target, speedMultipliers);
        }
        
        private void Init(GameObject target, SpeedMultipliers speedMultipliers) {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            _movementController = target.AddComponent<KeyboardMovementController>();
            _movementController.Init(speedMultipliers.Keyboard);
#elif UNITY_IOS
            _movementController = target.AddComponent<AccelerometerMovementController>();
            _movementController.Init(speedMultipliers.Accelerometer);
#else
            throw new Exception($"Ball movement not supported on the {Application.platform} platform");
#endif
        }
    }
}