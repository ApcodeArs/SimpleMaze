using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Services {
    public class MainUiService: MonoBehaviourCoreService {
        [SerializeField] private TextMeshProUGUI _levelLabel;
        [SerializeField] private Button _restartButton;

        private void Awake() {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        public override void Init() {
            InitLevelLabel();
        }

        private void InitLevelLabel() {
            var currentLevel = Core.Get<GameDataService>().GameData.Level;
            _levelLabel.text = $"Level {currentLevel}";
        }
        
        private void OnRestartButtonClick() { 
            Core.Get<GameService>().RestartLevel();
        }
    }
}