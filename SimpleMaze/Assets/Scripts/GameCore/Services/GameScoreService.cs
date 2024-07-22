using ScriptableObjects;
using UnityEngine;

namespace GameCore.Services {
    public class GameScoreService: MonoBehaviourCoreService {
        [SerializeField] private Points _pointsConfig;

        //todo cache services
        
        public void EarnOnLevelCompletePoints() {
            Core.Get<GameDataService>().AddPoints(_pointsConfig.LevelComplete);
        }

        public void EarnOnCoinCollectedPoints() {
            Core.Get<GameDataService>().AddPoints(_pointsConfig.CoinCollect, false);
        }
    }
}