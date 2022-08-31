using System;
using UnityEngine;

namespace Models {
    public class FinishHole: MonoBehaviour {
        [SerializeField] private Collider2D _collider;

        public Action OnBallInteraction;
        
        public void Init() {
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