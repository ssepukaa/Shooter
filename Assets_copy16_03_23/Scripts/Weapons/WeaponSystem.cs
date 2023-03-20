

using UnityEngine;

namespace Assets.Scripts.Weapons {
    public class WeaponSystem {
        [SerializeField] private float damage;
        [SerializeField] private float explosive;
        public WeaponSystem(float damage, float explosive) {
            this.damage=damage;
            this.explosive=explosive;
        }

        public float GetDamage() {
            return damage;}

        public float GetExplosive() {
            return explosive;
        }
    }
}
