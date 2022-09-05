using Helpers;
using UnityEngine;

namespace Models.GameObjectModels.Holes {
    public class DefaultHole: MonoBehaviour {
        public virtual void Init() {
            InitRandomRotation();
        }

        private void InitRandomRotation() {
            var randomAngle = RandomHelper.GetAngle(-180f, 180f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomAngle));
        }
    }
}