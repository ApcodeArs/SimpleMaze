using ScriptableObjects;
using UnityEngine;

namespace GameCore.Services {
    public class GameScoreService: MonoBehaviourCoreService {
        [SerializeField] private Points _pointsConfig;
        
        private GameDataService _gameDataService;
        
        public override void Init() {
            _gameDataService = Core.Get<GameDataService>();
        }

        public void EarnOnLevelCompletePoints() {
            _gameDataService.AddPoints(_pointsConfig.LevelComplete);
        }

        public void EarnOnCoinCollectedPoints() {
            _gameDataService.AddPoints(_pointsConfig.CoinCollect, false);
        }
    }
}