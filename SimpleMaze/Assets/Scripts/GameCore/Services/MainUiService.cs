using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Services {
    public class MainUiService: MonoBehaviourCoreService {
        [SerializeField] private Button _restartButton;

        public void Awake() {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick() { 
            Core.Get<GameService>().RestartLevel();
        }
    }
}