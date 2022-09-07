using System;
using UnityEngine;

namespace Models.GameObjects.Ball.MovementControllers {
    public class BallMovementController {
        private BaseMovementController _movementController;

        public BallMovementController(GameObject target) {
            Init(target);
        }
        
        private void Init(GameObject target) {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            _movementController = target.AddComponent<KeyboardMovementController>();
#elif UNITY_IOS
            _movementController = target.AddComponent<AccelerometerMovementController>();   
#else
            throw new Exception($"Ball movement not supported on the {Application.platform} platform");
#endif
        }
    }
}