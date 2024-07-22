using UnityEngine;

namespace Models.GameObjects {
    public abstract class BaseMazeObject: MonoBehaviour {
        public static readonly Vector2Int DefaultMazePosition = new (-1, 1);
        
        public Vector2Int MazePosition { get; private set; } = DefaultMazePosition;

        public virtual void Init(Vector2Int mazePosition) {
            MazePosition = mazePosition;
        }
    }
}