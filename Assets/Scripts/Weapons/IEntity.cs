using UnityEngine;

namespace Assets.Scripts.Weapons {
    public interface IEntity {
        void ApplyDamage(float damageValue);
        void Hit(Vector3 position, Quaternion rotation);
    }
}