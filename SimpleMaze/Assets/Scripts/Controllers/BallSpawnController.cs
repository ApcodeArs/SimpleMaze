using Helpers;
using UnityEngine;

namespace Controllers {
    public class BallSpawnController: MonoBehaviourSingletonBase<BallSpawnController> {
        [SerializeField] public GameObject _ball;

        public void SpawnOnStart() {
            var startPosition = MazeController.Instance.GetStartPosition();
            _ball.transform.position = new Vector3(startPosition.x, startPosition.y, _ball.transform.position.z);
        }
    }
}