using System.Collections.Generic;
using UnityEngine;

namespace Extensions {
    public static class ClearExtensions {
        public static void Clear(this GameObject[,] gameMatrix) {
            if (gameMatrix == null) {
                return;
            }

            for (var i = 0; i < gameMatrix.GetLength(0); i++) {
                for (var j = 0; j < gameMatrix.GetLength(1); j++) {
                    Object.Destroy(gameMatrix[i, j]);
                }
            }
        }

        public static void Clear(this List<GameObject> gameList) {
            if (gameList == null) {
                return;
            }
            
            for (var i = 0; i < gameList.Count; i++) {
                Object.Destroy(gameList[i]);
            }
            
            gameList.Clear();
        }
    }
}