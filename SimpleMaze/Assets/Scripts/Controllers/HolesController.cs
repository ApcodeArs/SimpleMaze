using System;
using Helpers;
using Models;
using UnityEngine;

namespace Controllers {
    public class HolesController: MonoBehaviourSingletonBase<HolesController> {
        [SerializeField] private GameObject _startHole;
        [SerializeField] private FinishHole _finishHole;
        
        public Action OnBallFinished;
        
        public void Init() {
            InitStartHole();
            InitFinishHole();
        }

        private void InitStartHole() {
            var startPosition = MazeController.Instance.GetStartPosition();
             _startHole.transform.position = new Vector3(startPosition.x, startPosition.y, _startHole.transform.position.z);
        }

        private void InitFinishHole() {
            var finishPosition = MazeController.Instance.GetFinishPosition();
            _finishHole.gameObject.transform.position = new Vector3(finishPosition.x, finishPosition.y, _finishHole.transform.position.z);
            _finishHole.OnBallInteraction += () =>  OnBallFinished?.Invoke();
        }
    }
}