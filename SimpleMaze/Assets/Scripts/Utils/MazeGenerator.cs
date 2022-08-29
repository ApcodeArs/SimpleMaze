using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Utils {
    public class MazeGenerator {
        public static MazeGeneratorCell[,] Generate(int width, int height) {
            var cells = new MazeGeneratorCell[width, height];

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    cells[x, y] = new MazeGeneratorCell(x, y);
                }
            }

            for (var x = 0; x < width; x++) {
                cells[x, height - 1].IsLeftWall = false;
            }

            for (var y = 0; y < height; y++) {
                cells[width - 1, y].IsBottomWall = false;
            }

            RemoveWallsWithBackTracker(cells, width, height);

            return cells;
        }

        private static void RemoveWallsWithBackTracker(MazeGeneratorCell[,] maze, int width, int height) {
            var current = maze[0, 0]; //todo improve
            current.IsVisited = true;
            current.DistanceFromStart = 0;

            var stack = new Stack<MazeGeneratorCell>();
            do {
                var unvisitedNeighbours = new List<MazeGeneratorCell>();

                var x = current.X;
                var y = current.Y;

                if (x > 0 && !maze[x - 1, y].IsVisited) unvisitedNeighbours.Add(maze[x - 1, y]);
                if (y > 0 && !maze[x, y - 1].IsVisited) unvisitedNeighbours.Add(maze[x, y - 1]);
                if (x < width - 2 && !maze[x + 1, y].IsVisited) unvisitedNeighbours.Add(maze[x + 1, y]);
                if (y < height - 2 && !maze[x, y + 1].IsVisited) unvisitedNeighbours.Add(maze[x, y + 1]);

                if (unvisitedNeighbours.Count > 0) {
                    var chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
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
        
        private static void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b) {
            if (a.X == b.X) {
                if (a.Y > b.Y) a.IsBottomWall = false;
                else b.IsBottomWall = false;
            }
            else {
                if (a.X > b.X) a.IsLeftWall = false;
                else b.IsLeftWall = false;
            }
        }
        
        private static Vector2Int PlaceMazeExit(MazeGeneratorCell[,] maze, int width, int height) {
            MazeGeneratorCell furthest = maze[0, 0];

            for (var x = 0; x < maze.GetLength(0); x++) {
                if (maze[x, height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, height - 2];
                if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
            }

            for (var y = 0; y < maze.GetLength(1); y++) {
                if (maze[width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[width - 2, y];
                if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
            }

            //todo improve
            if (furthest.X == 0) furthest.IsLeftWall = false;
            else if (furthest.Y == 0) furthest.IsBottomWall = false;
            else if (furthest.X == width - 2) maze[furthest.X + 1, furthest.Y].IsLeftWall = false;
            else if (furthest.Y == height - 2) maze[furthest.X, furthest.Y + 1].IsBottomWall = false;

            return new Vector2Int(furthest.X, furthest.Y);
        }
    }
}
