using System.Collections.Generic;
using Extensions;
using Models.Maze;
using UnityEngine;

namespace GameCore.MazeGenerators {
    public class SimpleMazeGenerator {
        private readonly MazeCellData[,] _cells;
        
        private readonly int _width;
        private readonly int _height;

        private List<Vector2Int> _possibleStartPositions;
        private Vector2Int _startPosition;
        
        public SimpleMazeGenerator(int width, int height) {
            _width = width;
            _height = height;
            
            _cells = new MazeCellData[_width, _height];

            InitPossibleStartPositions();
        }
        
        public Maze Generate() {
            CreateEmpty();
            InitBoundaryCells();

            _startPosition = GetStartPosition();
            RemoveWallsWithBackTracker();
            var finishPosition = GetFinishPosition();
            
            var maze = new Maze(_cells, _startPosition, finishPosition);
            
            return maze;
        }

        private void InitPossibleStartPositions() {
            _possibleStartPositions = new List<Vector2Int>() {
                new (0, 0),
                new (_width - 2, 0),
                new (0, _height - 2),
                new (_width - 2, _height - 2)
            };
        }
        
        private void CreateEmpty() {
            for (var x = 0; x < _width; x++) {
                for (var y = 0; y < _height; y++) {
                    _cells[x, y] = new MazeCellData(x, y);
                }
            }
        }

        private void InitBoundaryCells() {
            for (var x = 0; x < _width; x++) {
                _cells[x, _height - 1].IsLeftWall = false;
            }

            for (var y = 0; y < _height; y++) {
                _cells[_width - 1, y].IsBottomWall = false;
            }
        }
        
        //https://habr.com/ru/post/445378/
        private void RemoveWallsWithBackTracker() {
            var current = _cells[_startPosition.x, _startPosition.y];
            current.IsVisited = true;
            current.DistanceFromStart = 0;

            var stack = new Stack<MazeCellData>();
            do {
                var unvisitedNeighbours = new List<MazeCellData>();

                var x = current.X;
                var y = current.Y;

                if (x > 0 && !_cells[x - 1, y].IsVisited) unvisitedNeighbours.Add(_cells[x - 1, y]);
                if (y > 0 && !_cells[x, y - 1].IsVisited) unvisitedNeighbours.Add(_cells[x, y - 1]);
                if (x < _width - 2 && !_cells[x + 1, y].IsVisited) unvisitedNeighbours.Add(_cells[x + 1, y]);
                if (y < _height - 2 && !_cells[x, y + 1].IsVisited) unvisitedNeighbours.Add(_cells[x, y + 1]);

                if (unvisitedNeighbours.Count > 0) {
                    var chosen = unvisitedNeighbours.RandomElement();
                    RemoveWall(current, chosen);

                    chosen.IsVisited = true;
                    stack.Push(chosen);
                    chosen.DistanceFromStart = current.DistanceFromStart + 1;
                    current = chosen;
                }
                else {
                    current = stack.Pop();
                }
            } while (stack.Count > 0);
        }
        
        private void RemoveWall(MazeCellData a, MazeCellData b) {
            if (a.X == b.X) {
                if (a.Y > b.Y) a.IsBottomWall = false;
                else b.IsBottomWall = false;
            }
            else {
                if (a.X > b.X) a.IsLeftWall = false;
                else b.IsLeftWall = false;
            }
        }
        
        private Vector2Int GetStartPosition() => _possibleStartPositions.RandomElement();
        
        private Vector2Int GetFinishPosition() {
            var finishCell = _cells[_startPosition.x, _startPosition.y];
            
            for (var x = 0; x < _width - 1; x++) {
                for (var y = 0; y < _height - 1; y++) {
                    if (_cells[x, y].DistanceFromStart > finishCell.DistanceFromStart) {
                        finishCell = _cells[x, y];
                    }
                }
            }
            
            return new Vector2Int(finishCell.X, finishCell.Y);
        }
    }
}
