using Helpers;
using UnityEngine;

namespace Controllers {
    public class GameController: MonoBehaviourSingletonBase<GameController> {
        private void Awake() {
            PrepareGame();
            LoadLevel();
        }

        public void LoadLevel() {
            BackgroundController.Instance.Init();
            MazeController.Instance.GenerateMaze();
            HolesController.Instance.Init();
            BallSpawnController.Instance.SpawnOnStart();
        }
        
        private void PrepareGame() {
            MazeController.Instance.Init();
            HolesController.Instance.OnBallFinished += OnLevelEnd;
        }
        
        private void OnLevelEnd() {
            Debug.Log("Level End");
            LoadLevel();
        }
    }
}