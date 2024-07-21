using System.Collections.Generic;
using Models.GameObjects.Coin;
using UnityEngine;

namespace GameCore.Services {
    public class CoinsService: MonoBehaviourCoreService {
        [SerializeField] private Coin _coinPrefab; //todo
        
        [SerializeField] private List<Coin> _coins;
        
        public override void Init() {
            foreach (var coin in _coins) {
                InitCoin(coin);
            }
        }

        private void InitCoin(Coin coin) {
            coin.Init();
            coin.OnBallInteraction += OnBallInteraction;
        }

        private void OnBallInteraction(Coin coin)
        {
            Core.Get<GameScoreService>().EarnOnCoinCollectedPoints();
            Core.Get<MainUiService>().UpdateScore();
            
            //todo add sound
            Destroy(coin.gameObject);
        }
    }
}