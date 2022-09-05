using UnityEngine;

namespace GameCore.Services {
    public class GameService: MonoBehaviourCoreService {

        private void Start() {
            PrepareGame();
            LoadLevel();
        }
        
        public void LoadLevel() {
            Core.Get<BackgroundService>().Init();
            Core.Get<MazeService>().GenerateMaze();
            Core.Get<BallSpawnService>().SpawnOnStart();
            Core.Get<HolesService>().Init();
            Core.Get<MainUiService>().Init();
        }

        public void RestartLevel() {
            Debug.Log("Restart Level");
            Core.Get<BallSpawnService>().SpawnOnStart();
        }
        
        private void PrepareGame() {
            Core.Get<GameDataService>().Init();
            Core.Get<MazeService>().Init();
            Core.Get<HolesService>().OnBallFinished += OnLevelEnd;
        }
        
        private void OnLevelEnd() {
            Debug.Log("Level Complete!");
            Core.Get<GameDataService>().LevelUp();
            LoadLevel();
        }
    }
}