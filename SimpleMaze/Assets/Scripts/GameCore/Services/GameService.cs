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
            Core.Get<CoinsService>().Init();
            Core.Get<MainUiService>().Init();
        }

        public void RestartLevel() {
            Debug.Log("Restart Level");
            
            Core.Get<AudioService>().PlaySound("Restart", 0.0f, 0.5f);
            Core.Get<BallSpawnService>().SpawnOnStart();
        }
        
        private void PrepareGame() {
            Core.Get<GameSettingsService>().Init();
            Core.Get<GameDataService>().Init();
            Core.Get<MazeService>().Init();
            Core.Get<HolesService>().OnBallFinished += OnLevelEnd;
        }
        
        private void OnLevelEnd() {
            Debug.Log("Level Complete!");
            
            Core.Get<AudioService>().PlaySound("Complete", 0.0f, 0.5f);
            Core.Get<GameScoreService>().EarnOnLevelCompletePoints();
            Core.Get<GameDataService>().LevelUp();
            
            LoadLevel();
        }
    }
}