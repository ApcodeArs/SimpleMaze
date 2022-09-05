using System;
using Random = UnityEngine.Random;

namespace Helpers {
    public static class RandomHelper {
        public static float GetAngle(float start, float end) => Random.Range(start, end);
        
        public static float GetSeed() => (int)DateTime.Now.Ticks;
    }
}