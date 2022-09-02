using UnityEngine;

namespace GameCore {
    public abstract class MonoBehaviourCoreService: MonoBehaviour, ICoreService {
        public virtual void Init(){}
    }
}