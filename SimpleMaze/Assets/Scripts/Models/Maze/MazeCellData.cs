namespace Models.Maze {
    public class MazeCellData {
        public int X { get; }
        public int Y { get; }

        public bool IsLeftWall { get; set; } = true;
        public bool IsBottomWall { get; set; } = true;

        public bool IsVisited { get; set; }

        public int DistanceFromStart { get; set; }

        public MazeCellData(int x, int y) {
            X = x;
            Y = y;
        }
    }
}