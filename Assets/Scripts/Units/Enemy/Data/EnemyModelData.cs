using UnityEngine;

namespace Assets.Scripts.Units.Enemy.Data {
    [CreateAssetMenu(fileName = "Data", menuName = "Unit/Enemy")]
    public class EnemyModelData : ScriptableObject {
        public string nameModel;

        // Сделать ссылки на Data Scriptableobject
        [Header("VFX/SFX")] public AudioClip[] deathAudioClips;
        public AudioClip[] hitAudioClips;
        public GameObject onHitPrefab;

        public GameObject onDeadPrefab;
        // public AudioClip deathAudioClip;

        [Header("Stats")] public float health = 100f;
        [Header("Movement")] public float movementSpeed = 4f;
        public float rotationSpeed = 1f;
        public float jumpSpeed = 1.0F;
        public bool canMove = true;

        [Header("Weapon")] public float attackDistance = 0.5f;
        public float damageValue = 5;
        public float attackRate = 0.5f;
        public float nextAttackTime = 0;

        public AudioClip GetSoundOfDeath() {
            int randomClip = Random.Range(0, deathAudioClips.Length);
            return deathAudioClips[randomClip];
        }

        public AudioClip GetSoundOfHit() {
            int randomClip = Random.Range(0, hitAudioClips.Length);
            return hitAudioClips[randomClip];
        }
    }
}