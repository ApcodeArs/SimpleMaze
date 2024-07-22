using System;
using Helpers;
using Models.GameObjects;
using Models.GameObjects.Holes;
using UnityEngine;

namespace GameCore.Services {
    public class HolesService: MonoBehaviourCoreService {
        [SerializeField] private StartHole _startHole;
        [SerializeField] private FinishHole _finishHole;
        
        public event Action OnBallFinished;

        private MazeService _mazeService;
        
        private void Awake() {
            _finishHole.OnBallInteraction += _ => OnBallFinished?.Invoke();
        }

        public override void Init() {
            if (_mazeService == null) {
                _mazeService = Core.Get<MazeService>();
            }
            
            InitStartHole();
            InitFinishHole();
        }
        
        private void InitStartHole() {
            InitHole(_startHole, _mazeService.GetMazeStartCellPosition());
        }

        private void InitFinishHole() {
            InitHole(_finishHole, _mazeService.GetMazeFinishCellPosition());
        }

        private void InitHole(BaseMazeObject hole, Vector2Int mazeCellPosition) {
            var cellPosition = _mazeService.GetCellPosition(mazeCellPosition);
            
            InitHolePosition(hole.gameObject.transform, cellPosition);
            InitHoleRotation(hole.gameObject.transform);
            
            hole.Init(mazeCellPosition);
            _mazeService.SetMazeCell(hole.MazePosition);
        }

        private void InitHolePosition(Transform holeTransform, Vector3 position) {
            holeTransform.position = new Vector3(position.x, position.y, holeTransform.position.z);
        }

        private void InitHoleRotation(Transform holeTransform) {
            var randomAngle = RandomHelper.GetAngle(-180f, 180f);
            holeTransform.rotation = Quaternion.Euler(new Vector3(0, 0, randomAngle));
        }
    }
}