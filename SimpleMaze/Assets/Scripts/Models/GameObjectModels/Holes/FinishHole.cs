using System;
using UnityEngine;

namespace Models.GameObjectModels.Holes {
    public class FinishHole: DefaultHole {
        [SerializeField] private Collider2D _collider;

        public Action OnBallInteraction;
        
        public override void Init() {
            base.Init();
            SetInteractive(true);
        }

        private void SetInteractive(bool isActive) {
            _collider.enabled = isActive;
        }
        
        private void OnCollisionEnter2D(Collision2D col) {
            SetInteractive(false);
            OnBallInteraction?.Invoke();
        }
    }
}