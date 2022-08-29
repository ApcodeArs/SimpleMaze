using UnityEngine;

namespace Models {
    public class Cell : MonoBehaviour {
        [SerializeField]
        private GameObject _leftWall;
        [SerializeField]
        private GameObject _bottomWall;

        public void Init(MazeGeneratorCell mazeGeneratorCell) {
            _leftWall.SetActive(mazeGeneratorCell.IsLeftWall);
            _bottomWall.SetActive(mazeGeneratorCell.IsBottomWall);
        }
    }
}
