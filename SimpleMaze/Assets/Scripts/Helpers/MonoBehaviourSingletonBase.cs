using UnityEngine;

namespace Helpers {
    public abstract class MonoBehaviourSingletonBase<T> : MonoBehaviour
        where T : MonoBehaviourSingletonBase<T> {
        private static T _instance;

        public static T Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<T>();
                }
                
                return _instance;
            }
        }
    }
}
