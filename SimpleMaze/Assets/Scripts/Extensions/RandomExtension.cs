using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Extensions {
    public static class RandomExtension {
        public static T RandomElement<T>(this T[] array) => array[array.RandomIndex()];
        
        public static int RandomIndex<T>(this T[] array) => Random.Range(0, array.Length);

        public static T RandomElement<T>(this List<T> list) => list[list.RandomIndex()];

        public static int RandomIndex<T>(this List<T> list) => Random.Range(0, list.Count);

        public static IEnumerable<T> RandomSort<T>(this IEnumerable<T> collection) => collection.OrderBy(i => Random.Range(0, int.MaxValue));
    }
}