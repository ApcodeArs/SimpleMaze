using Extensions;
using Models;
using UnityEngine;
using Utils;

namespace GameCore.Services {

    public class MazeService : MonoBehaviourCoreService {
    
        [SerializeField] private Camera _camera;
        [SerializeField] private RectTransform _mazeParent;
        [SerializeField] private GameObject _cellPrefab;
        
        private int _mazeRowsCount;
        private int _mazeColumnsCount;
        
        private MazeCell[,] _cells;
        
        private Vector3 _cellSize;
        private Vector3 _cellOffset;

        private MazeGenerator _mazeGenerator;
        private Maze _maze;
        
        public override void Init() {
            var safeAreaWorldData = new SafeAreaWorldData(_camera);
            
            CalculateCellSize();
            CalculateMazeSize(safeAreaWorldData);
            
            _mazeGenerator = new MazeGenerator(_mazeColumnsCount, _mazeRowsCount);
            
            InitMazeParent(safeAreaWorldData);
            
            CalculateCellOffset();
        }

        public void GenerateMaze() {
            _maze = _mazeGenerator.Generate();

            var isCreateNew = CreateCellsIfNeeded();

            if (isCreateNew) {
                _cells.Loop((x, y) => {
                    InitNewCell(x, y, _maze.Cells[x, y]);
                });
            }
            else {
                _cells.Loop((x, y) => {
                    InitCell(_cells[x, y], _maze.Cells[x, y]);
                });
            }
        }
        
        public Vector3 GetStartPosition() => GetCellPosition(_maze.StartPosition);
        
        public Vector3 GetFinishPosition() => GetCellPosition(_maze.FinishPosition);

        private Vector3 GetCellPosition(Vector2Int cell) => _cells[cell.x, cell.y].transform.position + _cellSize / 2;

        private void CalculateCellSize() {
            _cellSize = _cellPrefab.transform.localScale;
        }
        
        private void CalculateMazeSize(SafeAreaWorldData safeAreaWorldData) {
            _mazeColumnsCount = Mathf.FloorToInt(safeAreaWorldData.Width / _cellSize.x);
            _mazeRowsCount = Mathf.FloorToInt(safeAreaWorldData.Height / _cellSize.y);
            
            //-1 due to fake cells
            Debug.Log($"Maze size: {_mazeColumnsCount - 1 } x {_mazeRowsCount - 1}");
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
        
        private bool CreateCellsIfNeeded() {
            if (_cells != null) {
                return false;
            }
            
            _cells = new MazeCell[_mazeColumnsCount,_mazeRowsCount];
            
            return true;
        }

        //todo improve
        private void InitNewCell(int x, int y, MazeCellData cellData) {
            var position = _cellOffset + new Vector3(x * _cellSize.x, y * _cellSize.y, 0.0f);
            var cellGameObject = Instantiate(_cellPrefab, position, Quaternion.identity, _mazeParent.transform);
            var cell = cellGameObject.GetComponent<MazeCell>();
            
            InitCell(cell, cellData);
                    
            _cells[x, y] = cell;
        }
        
        //todo improve
        private void InitCell(MazeCell cell, MazeCellData cellData) {
            cell.Init(cellData);
        }
    }
}