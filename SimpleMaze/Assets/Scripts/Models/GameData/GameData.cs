namespace Models.GameData {
    public struct GameData {
        public static readonly GameData Default = new(1, 0);
        
        public int Level { get; set; }
        public int Score { get; set; }

        private GameData(int level, int score) {
            Level = level;
            Score = score;
        }
    }
}