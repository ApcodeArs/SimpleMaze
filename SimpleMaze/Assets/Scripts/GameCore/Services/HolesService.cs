using System;
using Models;
using Models.GameObjectModels;
using UnityEngine;

namespace GameCore.Services {
    public class HolesService: MonoBehaviourCoreService {
        [SerializeField] private GameObject _startHole;
        [SerializeField] private FinishHole _finishHole;
        
        public Action OnBallFinished;

        private MazeService _mazeService;
        
        private void Awake() {
            _finishHole.OnBallInteraction += () => OnBallFinished?.Invoke();
        }

        public override void Init() {
            _mazeService = Core.Get<MazeService>();
            
            InitStartHole();
            InitFinishHole();
        }

        private void InitStartHole() {
            var startPosition = _mazeService.GetStartPosition();
             _startHole.transform.position = new Vector3(startPosition.x, startPosition.y, _startHole.transform.position.z);
        }

        private void InitFinishHole() {
            _finishHole.Init();
            
            var finishPosition = _mazeService.GetFinishPosition();
            _finishHole.gameObject.transform.position = new Vector3(finishPosition.x, finishPosition.y, _finishHole.transform.position.z);
        }
    }
}