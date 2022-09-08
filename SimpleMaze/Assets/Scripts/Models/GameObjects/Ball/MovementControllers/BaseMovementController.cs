using UnityEngine;

namespace Models.GameObjects.Ball.MovementControllers {
    public class BaseMovementController: MonoBehaviour {
        protected Rigidbody2D Rigidbody;

        protected float SpeedMultiplier;
        
        private void Awake() {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Init(float speedMultiplier) {
            SpeedMultiplier = speedMultiplier;
        }
    }
}