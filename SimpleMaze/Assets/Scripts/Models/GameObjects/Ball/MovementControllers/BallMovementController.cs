using System;
using UnityEngine;

namespace Models.GameObjects.Ball.MovementControllers {
    public class BallMovementController: MonoBehaviour {

        private BaseMovementController _movementController;
        
        private void Awake() {
            Init();
        }

        private void Init() {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            _movementController = gameObject.AddComponent<KeyboardMovementController>();
#elif UNITY_IOS
            _movementController = gameObject.AddComponent<AccelerometerMovementController>();   
#else
            throw new Exception($"Ball movement not supported on the {Application.platform} platform");
#endif
        }
    }
}