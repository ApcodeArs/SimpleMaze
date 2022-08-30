using System;
using UnityEngine;

namespace Extensions {
    public static class LoopExtensions {
        public static void Loop(this GameObject[,] gameMatrix, Action<int, int> callBack) {
            for (var i = 0; i < gameMatrix.GetLength(0); i++) {
                for (var j = 0; j < gameMatrix.GetLength(1); j++) {
                    callBack?.Invoke(i,j);
                }
            }
        }
    }
}