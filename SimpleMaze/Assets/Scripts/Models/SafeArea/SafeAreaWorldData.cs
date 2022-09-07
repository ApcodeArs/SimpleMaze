using UnityEngine;

namespace Models.SafeArea {
    public class SafeAreaWorldData {
        public Vector3 MinPoint { get; }
        public Vector3 MaxPoint { get; }
        
        public float Width { get; }
        public float Height { get; }
        
        public SafeAreaWorldData(Camera camera) {
            var rect = Screen.safeArea;
        
            MinPoint = camera.ScreenToWorldPoint(rect.position);
            MaxPoint = camera.ScreenToWorldPoint(rect.position + rect.size);
            
            Width = MaxPoint.x - MinPoint.x;
            Height = MaxPoint.y - MinPoint.y;
        }
    }
}