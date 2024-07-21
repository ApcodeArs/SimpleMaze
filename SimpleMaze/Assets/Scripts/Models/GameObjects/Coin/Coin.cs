using System;
using UnityEngine;

namespace Models.GameObjects.Coin {
    //todo move to common
    public class Coin: MonoBehaviour {
        [SerializeField] private Collider2D _collider;
        
        public event Action<Coin> OnBallInteraction;
        
        public void Init() {
            SetInteractive(true);
        }

        private void SetInteractive(bool isActive) {
            _collider.enabled = isActive;
        }
        
        private void OnCollisionEnter2D(Collision2D col) {
            SetInteractive(false);
            OnBallInteraction?.Invoke(this);
        }
    }
}