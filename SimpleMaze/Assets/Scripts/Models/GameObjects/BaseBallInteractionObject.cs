using System;
using UnityEngine;

namespace Models.GameObjects {
    public abstract class BaseBallInteractionObject: BaseMazeObject {
        private const string GameObjectBallName = "Ball";
        
        [SerializeField] private Collider2D _collider;

        public event Action<BaseBallInteractionObject> OnBallInteraction;

        public override void Init(Vector2Int mazePosition) {
            base.Init(mazePosition);
            SetInteractive(true);
        }
        
        private void SetInteractive(bool isActive) {
            _collider.enabled = isActive;
        }
        
        private void OnCollisionEnter2D(Collision2D col) {
            if (!col.gameObject.name.Equals(GameObjectBallName)) {
                return;
            }
            
            SetInteractive(false);
            OnBallInteraction?.Invoke(this);
        }
    }
}