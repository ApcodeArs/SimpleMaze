using UnityEngine;

namespace Models.GameObjects.Ball {
    public class Ball: MonoBehaviour {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private const int _speed = 5;

        private Vector2 _movementDirection;
        
        private void Update() {
            _movementDirection.x = 0;
            _movementDirection.y = 0;
            
            if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
                _movementDirection.x = -1;
            }
            if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
                _movementDirection.x = 1;
            }
            
            if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
                _movementDirection.y = 1;
            }
            if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
                _movementDirection.y = -1;
            }
        }

        private void FixedUpdate() {
            _rigidbody.velocity = _movementDirection * _speed;
        }
    }
}