using System.Collections.Generic;
using System.Linq;
using Extensions;
using GameCore.MazeGenerators;
using Models.SafeArea;
using Models.Maze;
using UnityEngine;

namespace GameCore.Services {

    public class MazeService : MonoBehaviourCoreService {
    
        [SerializeField] private Camera _camera;
        [SerializeField] private RectTransform _mazeParent;
        [SerializeField] private MazeCell _mazeCellPrefab;
        
        private int _mazeRowsCount;
        private int _mazeColumnsCount;
        
        private MazeCell[,] _cells;
        
        private Vector3 _cellSize;
        private Vector3 _cellOffset;

        private SimpleMazeGenerator _simpleMazeGenerator;
        private Maze _maze;
        
        public int GetCellsCount => (_mazeColumnsCount - 1) * (_mazeRowsCount - 1);
        
        public Vector2Int GetMazeStartCellPosition() => new(_maze.StartPosition.x, _maze.StartPosition.y);
        
        public Vector2Int GetMazeFinishCellPosition() => new(_maze.FinishPosition.x, _maze.FinishPosition.y);

        public Vector3 GetCellPosition(Vector2Int mazeCellPosition) =>
            _cells[mazeCellPosition.x, mazeCellPosition.y].transform.position + _cellSize / 2;
        
        public override void Init() {
            var safeAreaWorldData = new SafeAreaWorldData(_camera);
            
            CalculateCellSize();
            CalculateMazeSize(safeAreaWorldData);
            
            _simpleMazeGenerator = new SimpleMazeGenerator(_mazeColumnsCount, _mazeRowsCount);
            
            InitMazeParent(safeAreaWorldData);
            
            CalculateCellOffset();
        }

        public void GenerateMaze() {
            _maze = _simpleMazeGenerator.Generate();

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
        
        public List<Vector2Int> GetMazeEmptyCellsPositions() {
            var emptyCells = _maze.Cells.Cast<MazeCellData>()
                .Where(c => c.X != _mazeColumnsCount - 1 && c.Y != _mazeRowsCount - 1 && c.IsEmpty)
                .Select(c=>new Vector2Int(c.X, c.Y)).ToList();
            
            return emptyCells;
        }
        
        public void SetMazeCell(Vector2Int mazePosition, bool isEmpty = false) {
            _maze.Cells[mazePosition.x, mazePosition.y].IsEmpty = isEmpty;
        }
        
        private void CalculateCellSize() {
            _cellSize = _mazeCellPrefab.gameObject.transform.localScale;
        }
        
        private void CalculateMazeSize(SafeAreaWorldData safeAreaWorldData) {
            _mazeColumnsCount = Mathf.FloorToInt(safeAreaWorldData.Width / _cellSize.x);
            _mazeRowsCount = Mathf.FloorToInt(safeAreaWorldData.Height / _cellSize.y);
            
            //-1 due to fake cells
            Debug.Log($"Maze size: {_mazeColumnsCount - 1} x {_mazeRowsCount - 1}");
        }
        
        private void InitMazeParent(SafeAreaWorldData safeAreaWorldData) {
            //-1 due to fake cells
            _mazeParent.sizeDelta = new Vector2(_cellSize.x * (_mazeColumnsCount - 1), _cellSize.y * (_mazeRowsCount - 1));
            
            var yShifting = (safeAreaWorldData.Height  - _mazeParent.sizeDelta.y) / 2;
            
            _mazeParent.transform.position = new Vector3(safeAreaWorldData.MinPoint.x, safeAreaWorldData.MinPoint.y, 0.0f) + 
                                             new Vector3(safeAreaWorldData.Width / 2, safeAreaWorldData.Height / 2, 0.0f) +
                                             new Vector3(0.0f, - yShifting, 0.0f);
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
        
        private void InitNewCell(int x, int y, MazeCellData cellData) {
            var position = _cellOffset + new Vector3(x * _cellSize.x, y * _cellSize.y, 0.0f);
            var cell = Instantiate(_mazeCellPrefab, position, Quaternion.identity, _mazeParent.transform);
            
            InitCell(cell, cellData);
            _cells[x, y] = cell;
        }
        
        private void InitCell(MazeCell cell, MazeCellData cellData) {
            cell.Init(cellData);
        }
    }
}