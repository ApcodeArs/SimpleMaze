using UnityEngine;

namespace Models.GameObjects.Ball.MovementControllers {
    public class AccelerometerMovementController: BaseMovementController {
        //todo check
        private void Update() {
            Rigidbody.AddForce(Input.acceleration);
        }
    }
}