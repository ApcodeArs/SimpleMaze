using System.Collections.Generic;
using System.Linq;
using Extensions;
using Models.GameObjects;
using Models.GameObjects.Coin;
using ScriptableObjects;
using UnityEngine;

namespace GameCore.Services {
    public class CoinsService: MonoBehaviourCoreService {
        [SerializeField] private Coins _coinsConfig;
        
        [SerializeField] private Transform _coinsParent;
        [SerializeField] private Coin _coinPrefab;
        
        private MazeService _mazeService;
        private GameScoreService _gameScoreService;
        private MainUiService _mainUiService;
        
        private List<Coin> _coinsPool;
        
        public override void Init() {
            _mazeService = Core.Get<MazeService>();
            _gameScoreService = Core.Get<GameScoreService>();
            _mainUiService = Core.Get<MainUiService>();
            
            CreateCoinsPool();
        }
        
        private void CreateCoinsPool() {
            var coinsPoolCount = Mathf.CeilToInt(_mazeService.GetCellsCount * _coinsConfig.MazeMaxCoverage);
            Debug.Log($"Coins pool count: {coinsPoolCount}");
            
            _coinsPool = new List<Coin>();
            
            for (var i = 0; i < coinsPoolCount; i++) {
                var coin = Instantiate(_coinPrefab, _coinsParent);
                coin.gameObject.SetActive(false);
                
                _coinsPool.Add(coin);
            }
        }
        
        public void GenerateCoins() {
            ResetCoins(); //todo improve
            
            var coinsRandomCoverage = Random.Range(_coinsConfig.MazeMinCoverage, _coinsConfig.MazeMaxCoverage);
            Debug.Log($"Coins coverage: {coinsRandomCoverage:F4}");
            
            var emptyCellsPositions = _mazeService.GetMazeEmptyCellsPositions();
            var coinsCount = Mathf.FloorToInt(emptyCellsPositions.Count * coinsRandomCoverage);
            
            for (var i = 0; i < coinsCount; i++) {
                var rndEmptyCellPosition = emptyCellsPositions.RandomElement();

                var coin = _coinsPool.First(c => !c.gameObject.activeInHierarchy);
                InitCoin(coin, rndEmptyCellPosition);
                
                _mazeService.SetMazeCell(coin.MazePosition);
                emptyCellsPositions.Remove(coin.MazePosition);
            }
        }
        
        private void InitCoin(Coin coin, Vector2Int mazeCellPosition) {
            coin.Init(mazeCellPosition);
            coin.OnBallInteraction += OnBallInteraction;
            
            var position = _mazeService.GetCellPosition(mazeCellPosition);
            coin.gameObject.transform.position = new Vector3(position.x, position.y, _coinPrefab.gameObject.transform.position.z);
            coin.gameObject.SetActive(true);
        }

        private void OnBallInteraction(BaseBallInteractionObject ballInteractionObject) {
            _gameScoreService.EarnOnCoinCollectedPoints();
            _mainUiService.UpdateScore();
            
            //todo add sound
            ResetCoin(ballInteractionObject);
        }

        private void ResetCoins() {
            foreach (var coin in _coinsPool) {
                ResetCoin(coin);
            }
        }
        
        private void ResetCoin(BaseBallInteractionObject ballInteractionObject) {
            ballInteractionObject.gameObject.SetActive(false);
            ballInteractionObject.gameObject.transform.position = _coinPrefab.gameObject.transform.position;
            
            ballInteractionObject.OnBallInteraction -= OnBallInteraction;
            
            if (ballInteractionObject.MazePosition != BaseMazeObject.DefaultMazePosition) {
                _mazeService.SetMazeCell(ballInteractionObject.MazePosition, true);
            }
        }
    }
}