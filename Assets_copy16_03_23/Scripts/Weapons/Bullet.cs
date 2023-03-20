using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons {
    [RequireComponent(typeof(CapsuleCollider))]
    public class Bullet : MonoBehaviour, IBullet {
        public float bulletSpeed;
        public float hitForce;
        public float destroyAfter;

        float currentTime = 0;
        Vector3 newPos;
        Vector3 oldPos;
        bool hasHit = false;

        float damageAmount;

        private void Start() {
            StartCoroutine(DestroyBullet());
        }

        private void OnCollisionEnter(Collision collision) {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;


            IEntity entity = collision.transform.GetComponent<IEntity>();
            if (entity != null) {
                entity.Hit(position, rotation);
                entity.ApplyDamage(damageAmount);
            }

            Destroy(gameObject);
        }


        public void SetDamage(float damageValue) {
            damageAmount = damageValue;
        }

        IEnumerator DestroyBullet() {
            hasHit = true;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}