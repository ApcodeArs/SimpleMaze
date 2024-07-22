using UnityEngine;

namespace GameCore.Services {
    public class BallSpawnService: MonoBehaviourCoreService {
        [SerializeField] public GameObject _ball;

        private MazeService _mazeService;
        
        public override void Init() {
            _mazeService = Core.Get<MazeService>();
        }

        public void SpawnOnStart() {
            var startPosition = _mazeService.GetCellPosition(_mazeService.GetMazeStartCellPosition());
            _ball.transform.position = new Vector3(startPosition.x, startPosition.y, _ball.transform.position.z);
        }
    }
}