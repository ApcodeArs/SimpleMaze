namespace Models {
    public class MazeGeneratorCell {
        public int X { get; }
        public int Y { get; }

        public bool IsLeftWall { get; set; } = true;
        public bool IsBottomWall { get; set; } = true;

        public bool IsVisited { get; set; }

        public int DistanceFromStart { get; set; }

        public MazeGeneratorCell(int x, int y) {
            X = x;
            Y = y;
        }
    }
}