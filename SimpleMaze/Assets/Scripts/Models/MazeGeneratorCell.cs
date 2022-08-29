namespace Models {
    public class MazeGeneratorCell {
        public int X { get; }
        public int Y { get; }

        public bool IsLeftWall = true;
        public bool IsBottomWall = true;

        public bool IsVisited;

        public int DistanceFromStart;

        public MazeGeneratorCell(int x, int y) {
            X = x;
            Y = y;
        }
    }
}