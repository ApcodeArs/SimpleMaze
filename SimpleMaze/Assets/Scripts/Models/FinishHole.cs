using System;
using UnityEngine;

namespace Models {
    public class FinishHole: MonoBehaviour {
        public Action OnBallInteraction;

        private void OnCollisionEnter2D(Collision2D col) {
            //todo disable collider
            OnBallInteraction?.Invoke();
        }
    }
}