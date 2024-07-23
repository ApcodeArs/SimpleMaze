using Models.GameData;
using UnityEditor;
using UnityEngine;

namespace GameCore.Services {
    public class GameDataService: MonoBehaviourCoreService {
        private const string LevelKey = "Level";
        private const string ScoreKey = "Score";

        private bool _isChanged;
        
        private GameData _gameData;

        public GameData GameData => _gameData;
        
#if UNITY_EDITOR      
        private void Awake() {
            EditorApplication.playModeStateChanged += state => {
                if (state != PlayModeStateChange.ExitingPlayMode) {
                    return;
                }
                
                Save();
            };
        }
#endif
        
        public override void Init() {
            _gameData = GameData.Default;

            TryLoadLevel();
            TryLoadScore();
        }

        public void ResetLevelScore() {
            TryLoadScore();
        }
        
        public void LevelUp() {
            _gameData.Level++;
            _isChanged = true;
            
            Save();
        }
        
        public void AddPoints(int points, bool isChanged = true) {
            _gameData.Score += points;
            _isChanged = isChanged;
        }
        
#if UNITY_EDITOR  
        public void Reset() {
            Debug.Log("Reset Game Data");
            PlayerPrefs.DeleteAll();
        }
#endif
        
        private bool TryLoadLevel() {
            if (!PlayerPrefs.HasKey(LevelKey)) {
                return false;
            }
            
            _gameData.Level = PlayerPrefs.GetInt(LevelKey);
            return true;

        }

        private bool TryLoadScore() {
            if (!PlayerPrefs.HasKey(ScoreKey)) {
                return false;
            }
            
            _gameData.Score = PlayerPrefs.GetInt(ScoreKey);
            return true;
        }
        
        private void Save() {
            if (!_isChanged) {
                return;
            }
            
            Debug.Log("Save Game Data");
            
            PlayerPrefs.SetInt(LevelKey, _gameData.Level);
            PlayerPrefs.SetInt(ScoreKey, _gameData.Score);

            _isChanged = false;
        }

        private void OnApplicationPause(bool isPause) {
            if (!isPause) {
                return;
            }
            
            Save();
        }
    }
}