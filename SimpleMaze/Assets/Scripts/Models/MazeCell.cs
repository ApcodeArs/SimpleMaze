using UnityEngine;

namespace Models {
    public class MazeCell : MonoBehaviour {
        [SerializeField] private GameObject _leftWall;
        [SerializeField] private GameObject _bottomWall;

        public void Init(MazeCellData mazeGeneratorCell) {
            _leftWall.SetActive(mazeGeneratorCell.IsLeftWall);
            _bottomWall.SetActive(mazeGeneratorCell.IsBottomWall);
        }
    }
}
