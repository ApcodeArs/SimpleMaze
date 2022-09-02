using System;
using Models.GameObjectModels.Holes;
using UnityEngine;

namespace GameCore.Services {
    public class HolesService: MonoBehaviourCoreService {
        [SerializeField] private DefaultHole _startHole;
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
            InitHole(_startHole, _mazeService.GetStartPosition());
        }

        private void InitFinishHole() {
            InitHole(_finishHole, _mazeService.GetFinishPosition());
        }

        private void InitHole(DefaultHole hole, Vector3 position) {
            hole.Init();
            hole.transform.position = new Vector3(position.x, position.y, _startHole.transform.position.z);
        }
    }
}