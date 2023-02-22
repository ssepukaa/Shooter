using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Units.Enviroment {
    public class Block : Unit, IEntity {
        public GameObject prefabHit;
        //public AudioClip audioClip;
        //public float volume = 1f;

        public void ApplyDamage(float damageValue) { }

        public void Hit(Vector3 position, Quaternion rotation) {
            Instantiate(prefabHit, position, rotation);

            //AudioSource.PlayClipAtPoint(audioClip,position,volume);
        }
    }
}