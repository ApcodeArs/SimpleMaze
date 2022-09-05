using ScriptableObjects;
using UnityEngine;

namespace GameCore.Services {
    public class GameScoreService: MonoBehaviourCoreService {
        [SerializeField] private Points _points;

        public void EarnOnLevelCompletePoints() {
            Core.Get<GameDataService>().AddPoints(_points.LevelComplete);
        }
    }
}