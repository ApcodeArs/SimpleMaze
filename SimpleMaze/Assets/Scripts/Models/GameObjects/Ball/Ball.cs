using Models.GameObjects.Ball.MovementControllers;
using ScriptableObjects;
using UnityEngine;

namespace Models.GameObjects.Ball {
    public class Ball: MonoBehaviour {
        [SerializeField] private SpeedMultipliers _speedMultipliers; //todo move to BallMovementController
        
        private BallMovementController _movementController;

        private void Awake() {
            InitMovementController();
        }

        private void InitMovementController() {
            _movementController = new BallMovementController(gameObject, _speedMultipliers);
        }
    }
}