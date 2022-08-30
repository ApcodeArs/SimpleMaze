using Extensions;
using Helpers;
using Models;
using UnityEngine;
using Utils;

namespace Controllers {

    public class MazeController : MonoBehaviourSingletonBase<MazeController> {
    
        [SerializeField] private Camera _camera;
        [SerializeField] private RectTransform _mazeParent;
        [SerializeField] private GameObject _cellPrefab;
        
        private int _mazeRowsCount;
        private int _mazeColumnsCount;
        
        private GameObject[,] _cells;
        
        private Vector3 _cellSize;
        private Vector3 _cellOffset;

        private MazeGenerator _mazeGenerator;
        
        public void Init() {
            var safeAreaWorldData = new SafeAreaWorldData(_camera);
            
            CalculateCellSize();
            CalculateMazeSize(safeAreaWorldData);
            
            _mazeGenerator = new MazeGenerator(_mazeColumnsCount, _mazeRowsCount);
            
            InitMazeParent(safeAreaWorldData);
            
            CalculateCellOffset();
            
            InitMaze();
        }

        private void CalculateCellSize() {
            _cellSize = _cellPrefab.transform.localScale;
        }
        
        private void CalculateMazeSize(SafeAreaWorldData safeAreaWorldData) {
            _mazeColumnsCount = Mathf.FloorToInt(safeAreaWorldData.Width / _cellSize.x);
            _mazeRowsCount = Mathf.FloorToInt(safeAreaWorldData.Height / _cellSize.y);
        }
        
        private void InitMazeParent(SafeAreaWorldData safeAreaWorldData) {
            //-1 due to fake cells
            _mazeParent.sizeDelta = new Vector2(_cellSize.x * (_mazeColumnsCount - 1), _cellSize.y * (_mazeRowsCount - 1));
            
            _mazeParent.transform.position = new Vector3(safeAreaWorldData.MinPoint.x, safeAreaWorldData.MinPoint.y, 0.0f) + 
                                             new Vector3(safeAreaWorldData.Width / 2, safeAreaWorldData.Height / 2, 0.0f);
        }

        private void CalculateCellOffset() {
            var mazeParentSize = _mazeParent.sizeDelta;
            _cellOffset = _mazeParent.transform.position + new Vector3(-mazeParentSize.x / 2f, -mazeParentSize.y / 2f);
        }
        
        private void InitMaze() {
            var maze = _mazeGenerator.Generate();

            var isCreateNew = CreateCellsIfNeeded();

            if (isCreateNew) {
                _cells.Loop((x, y) => {
                    InitNewCell(x, y, maze.Cells[x, y]);
                });
            }
            else {
                _cells.Loop((x, y) => {
                    InitCell(_cells[x, y], maze.Cells[x, y]);
                });
            }
        }

        private bool CreateCellsIfNeeded() {
            if (_cells != null) {
                return false;
            }
            
            _cells = new GameObject[_mazeColumnsCount,_mazeRowsCount];
            
            return true;
        }

        //todo improve
        private void InitNewCell(int x, int y, MazeCellData cellData) {
            var position = _cellOffset + new Vector3(x * _cellSize.x, y * _cellSize.y, 0.0f);
            var cellGameObject = Instantiate(_cellPrefab, position, Quaternion.identity, _mazeParent.transform);
                    
            InitCell(cellGameObject, cellData);
                    
            _cells[x, y] = cellGameObject;
        }
        
        //todo improve
        private void InitCell(GameObject cellGameObject, MazeCellData cellData) {
            var cell = cellGameObject.GetComponent<MazeCell>();
            cell.Init(cellData);
        }
    }
}
