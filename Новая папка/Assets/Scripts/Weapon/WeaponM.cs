using UnityEngine;

namespace Assets.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "Data", menuName = "Weapon/WeaponModel", order = 2)]
    public class WeaponM: ScriptableObject
    {
        public string nameWeaponModel;
        public bool singleFire = false;
        public float fireRate = 0.1f;
        public GameObject bulletPrefab;
        //public GameObject weaponPrefab;
        public int bulletsPerMagazine = 30;
       
        public float timeToReload = 1.5f;
        public float weaponDamage = 15; //How much damage should this weapon deal
        public AudioClip fireAudio;
        public AudioClip reloadAudio;
        public float bulletSpeed = 300f;
        public bool canFire = true;
        public float nextFireTime = 0;
    }
}