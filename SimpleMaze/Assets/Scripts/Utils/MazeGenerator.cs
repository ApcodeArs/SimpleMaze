using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Utils {
    public class MazeGenerator {
        private readonly MazeCellData[,] _cells;
        
        private readonly int _width;
        private readonly int _height;
        
        public MazeGenerator(int width, int height) {
            _width = width;
            _height = height;
            
            _cells = new MazeCellData[_width, _height];
        }
        
        public Maze Generate() {
            CreateEmpty();
            InitBoundaryCells();

            var startPosition = CreateStartPosition();
            RemoveWallsWithBackTracker(startPosition);
            var finishPosition = GetFinishPosition(startPosition);
            
            var maze = new Maze(_cells, startPosition, finishPosition);
            
            return maze;
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

        //todo improve
        private Vector2Int CreateStartPosition() {
            var startCell = _cells[0, 0];
            return new Vector2Int(startCell.X, startCell.Y);
        }
        
        //https://habr.com/ru/post/445378/
        private void RemoveWallsWithBackTracker(Vector2Int startPosition) {
            var current = _cells[startPosition.x, startPosition.y];
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
                    var chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
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
        
        //todo check
        private Vector2Int GetFinishPosition(Vector2Int startPosition) {
            var furthest = _cells[startPosition.x, startPosition.y];

            for (var x = 0; x < _cells.GetLength(0); x++) {
                if (_cells[x, _height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = _cells[x, _height - 2];
                if (_cells[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = _cells[x, 0];
            }

            for (var y = 0; y < _cells.GetLength(1); y++) {
                if (_cells[_width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = _cells[_width - 2, y];
                if (_cells[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = _cells[0, y];
            }
            
            return new Vector2Int(furthest.X, furthest.Y);
        }
    }
}
