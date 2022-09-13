using UnityEngine;

namespace GameCore.Services {
    public class GameSettingsService: MonoBehaviourCoreService {
        
        public override void Init() {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}