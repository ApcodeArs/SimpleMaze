using Helpers;

namespace Controllers {
    public class GameController: MonoBehaviourSingletonBase<GameController> {
        private void Awake() {
            MazeController.Instance.Init();
        }
    }
}