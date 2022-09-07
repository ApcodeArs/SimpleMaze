using Models.GameObjects.Ball.MovementControllers;
using UnityEngine;

namespace Models.GameObjects.Ball {
    public class Ball: MonoBehaviour {
        private BallMovementController _movementController;

        private void Awake() {
            InitMovementController();
        }

        private void InitMovementController() {
            _movementController = new BallMovementController(gameObject);
        }
    }
}