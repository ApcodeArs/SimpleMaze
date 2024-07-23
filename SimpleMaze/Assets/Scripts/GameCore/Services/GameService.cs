using Models.Audio;
using UnityEngine;

namespace GameCore.Services {
    public class GameService: MonoBehaviourCoreService {

        private void Start() {
            PrepareGame();
            LoadLevel(true);
        }
        
        public void LoadLevel(bool isFirstLoading = false) {
            Core.Get<BackgroundService>().Init();
            Core.Get<MazeService>().GenerateMaze();
            Core.Get<BallSpawnService>().SpawnOnStart();
            Core.Get<HolesService>().Init();
            Core.Get<CoinsService>().GenerateCoins(!isFirstLoading);
            Core.Get<MainUiService>().Init();
        }

        public void RestartLevel() {
            Debug.Log("Restart Level");
            
            Core.Get<AudioService>().PlaySound(SoundId.Restart, 0.0f, 0.5f);
            Core.Get<BallSpawnService>().SpawnOnStart();
            Core.Get<CoinsService>().GenerateCoins();
            Core.Get<GameDataService>().ResetLevelScore();
            Core.Get<MainUiService>().UpdateScore();
        }
        
        private void PrepareGame() {
            Core.Get<GameSettingsService>().Init();
            Core.Get<GameDataService>().Init();
            Core.Get<MazeService>().Init();
            Core.Get<BallSpawnService>().Init();
            Core.Get<HolesService>().OnBallFinished += OnLevelEnd;
            Core.Get<CoinsService>().Init();
            Core.Get<GameScoreService>().Init();
        }
        
        private void OnLevelEnd() {
            Debug.Log("Level Complete!");
            
            Core.Get<AudioService>().PlaySound(SoundId.Complete, 0.0f, 0.5f);
            Core.Get<GameScoreService>().EarnOnLevelCompletePoints();
            Core.Get<GameDataService>().LevelUp();
            
            LoadLevel();
        }
    }
}