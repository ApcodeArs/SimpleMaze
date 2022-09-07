using UnityEngine;

namespace Models.GameObjects.Ball.MovementControllers {
    public class BaseMovementController: MonoBehaviour {
        protected Rigidbody2D Rigidbody;

        private void Awake() {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}