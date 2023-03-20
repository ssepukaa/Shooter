using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Weapons {
    [RequireComponent(typeof(SphereCollider))]
   
    public class BulletRPG : MonoBehaviour,IBullet {
        public float bulletSpeed;
        public float hitForce;
        public float destroyAfter;
        private SphereCollider _sphereCollider;
        float currentTime = 0;
        Vector3 newPos;
        Vector3 oldPos;
        bool hasHit = false;

        float damageAmount;
        [SerializeField]private GameObject _prefabExposive;
        [SerializeField]private AudioClip _audioExposive;
        //private SoundManager _audioSource;
        

        private void Start() {
            StartCoroutine(DestroyBullet());
            
            _sphereCollider = GetComponent<SphereCollider>();
           // _audioSource = FindObjectOfType<SoundManager>();
            
            
        }

        private void OnTriggerEnter(Collider collider) {
            _sphereCollider.radius = 3f;
            _sphereCollider.isTrigger = false;
            Instantiate (_prefabExposive, transform.position, transform.rotation);
           // _audioSource.PlaySound(_audioExposive,1f,1f);
        }

        private void OnCollisionEnter(Collision collision) {
            ContactPoint[] contactsList = collision.contacts;
            foreach (var contactPoint in contactsList) {
               // Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
               // Vector3 position = contactPoint.point;
                IEntity entity = collision.transform.GetComponent<IEntity>();
                IPlayer entityPlayer = collision.transform.GetComponent<IPlayer>();
                if (entity != null && entityPlayer == null) {
                    entity.Hit(collision.transform.position, collision.transform.rotation);
                    entity.ApplyDamage(damageAmount);
                }


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