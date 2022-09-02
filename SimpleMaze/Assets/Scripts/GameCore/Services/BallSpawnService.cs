using UnityEngine;

namespace GameCore.Services {
    public class BallSpawnService: MonoBehaviourCoreService {
        [SerializeField] public GameObject _ball;

        public void SpawnOnStart() {
            var startPosition = Core.Get<MazeService>().GetStartPosition();
            _ball.transform.position = new Vector3(startPosition.x, startPosition.y, _ball.transform.position.z);
        }
    }
}