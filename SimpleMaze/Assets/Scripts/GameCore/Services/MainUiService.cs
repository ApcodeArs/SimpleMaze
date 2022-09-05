using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Services {
    public class MainUiService: MonoBehaviourCoreService {
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private TextMeshProUGUI _levelLabel;
        [SerializeField] private Button _restartButton;

        private void Awake() {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        public override void Init() {
            var gameData = Core.Get<GameDataService>().GameData;
            
            InitLevelLabel(gameData.Level);
            InitScoreLabel(gameData.Score);
        }

        private void InitLevelLabel(int level) {
            _levelLabel.text = $"Level {level}";
        }

        private void InitScoreLabel(int score) {
            _scoreLabel.text = score.ToString();
        }
        
        private void OnRestartButtonClick() { 
            Core.Get<GameService>().RestartLevel();
        }
    }
}