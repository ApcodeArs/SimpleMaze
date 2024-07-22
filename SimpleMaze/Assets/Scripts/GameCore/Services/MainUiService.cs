using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Services {
    public class MainUiService: MonoBehaviourCoreService {
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private TextMeshProUGUI _levelLabel;
        [SerializeField] private Button _restartButton;

        private GameService _gameService;
        private GameDataService _gameDataService;
        
        private void Awake() {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        public override void Init() {
            _gameService = Core.Get<GameService>();
            _gameDataService = Core.Get<GameDataService>();
            
            var gameData = _gameDataService.GameData;
            
            InitLevelLabel(gameData.Level);
            InitScoreLabel(gameData.Score);
        }

        public void UpdateScore() {
            var gameData =_gameDataService.GameData;
            UpdateScoreLabel(gameData.Score);
        }
        
        private void InitLevelLabel(int level) {
            _levelLabel.text = $"Level {level}";
        }

        private void InitScoreLabel(int score) => UpdateScoreLabel(score);
        
        private void UpdateScoreLabel(int score) {
            _scoreLabel.text = score.ToString();
        }
        
        private void OnRestartButtonClick() { 
            _gameService.RestartLevel();
        }
    }
}