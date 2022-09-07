using UnityEngine;

namespace Models.Maze {
    public class Maze {
        public MazeCellData[,] Cells { get; }
        
        public Vector2Int StartPosition { get; }
        public Vector2Int FinishPosition { get; }

        public Maze(MazeCellData[,] cells, Vector2Int startPosition, Vector2Int finishPosition) {
            Cells = cells;

            StartPosition = startPosition;
            FinishPosition = finishPosition;
        }
    }
}