using UnityEngine;

namespace Models.GameObjects.Ball.MovementControllers {
    public class AccelerometerMovementController: BaseMovementController {
        private void Update() {
            Rigidbody.AddForce(Input.acceleration * SpeedMultiplier);
        }
    }
}