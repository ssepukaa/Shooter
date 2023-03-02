using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.UI.Weapon;
using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Weapons {
    //[RequireComponent(typeof(AudioSource))]
    public class Weapon : Actor {
        public WeaponM _weaponM;
       // protected WeaponManager _weaponManager;
        protected Transform _muzzleTransform;
        [SerializeField] private float _scatter = 0.1f;
        private AudioSource _audioSource;
        private IBulletChangedUI[] _bulletChangedUIArray;

        private Player _player;
        //private float _minScatter = 0.1f;
        // private float _maxScatter = 0.2f;
        [Header("Weapon")]
        [SerializeField] private float fireRateFinal;
        [SerializeField] private float timeToReloadFinal;
        [SerializeField] private float weaponDamageFinal;
        [SerializeField] private float bulletSpeedFinal;
        [SerializeField] private bool canFireFinal = true;
        [SerializeField] private float nextFireTime = 0;
        [SerializeField] private int bulletsInMagazine;
        [SerializeField] private int bulletsInInventory = 10000;


        void Start() {
            _player = GetComponent<Player>();

            _muzzleTransform = _player.GetMuzzleTransform();
         //   _weaponManager = FindObjectOfType<WeaponManager>();

            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            //Make sound 3D
            _audioSource.spatialBlend = 1f;
            _bulletChangedUIArray = FindObjectsOfType<MonoBehaviour>().OfType<IBulletChangedUI>().ToArray();

        }

        void Update() {
            // if (Input.GetMouseButtonDown(0) && _weaponM.singleFire)
            // {
            //     Fire();
            // }
            // if (Input.GetMouseButton(0) && !_weaponM.singleFire)
            // {
            //     Fire();
            // }
            if (Input.GetKeyDown(KeyCode.R) && canFireFinal) {
                ReloadWeapon();
            }
            // if (SimpleInput.GetAxis("HorizontalRight") > 0.9f || SimpleInput.GetAxis("HorizontalRight") < -0.9f ||
            //     SimpleInput.GetAxis("VerticalRight") > 0.9f || SimpleInput.GetAxis("VerticalRight") < -0.9f)
            // {
            //     Fire();
            //
            // }

            nextFireTime += Time.deltaTime;
        }

        public void FireWeapon() {
            Fire();
        }

        protected void Fire() {
            if (canFireFinal) {
                if (nextFireTime > GetFireRateFinal()) {
                    nextFireTime = 0;

                    if (bulletsInMagazine > 0) {
                        //Point fire point at the current center of Camera

                        Vector3 fireVector = FireDirectVector();

                        // Разброс пуль

                        Vector3 randomAngle = fireVector;
                        randomAngle.z += Random.Range(-_scatter, _scatter);

                        //Fire
                        bulletsInMagazine--;
                        OnPlayerBulletsValueChanged();

                        GameObject newBullet = Instantiate<GameObject>(
                            _weaponM.bulletPrefab,
                            _muzzleTransform.position,
                            Quaternion.identity);

                        
                        var angleRandomForward = Quaternion.Euler(0, 0, randomAngle.z);
                        var bulletRigidbody = newBullet.GetComponent<Rigidbody>();
                        
                        bulletRigidbody.velocity = angleRandomForward * fireVector * _weaponM.bulletSpeed;


                        //Set bullet damage according to weapon damage value
                        newBullet.GetComponent<Bullet>().SetDamage(_weaponM.weaponDamage);

                        GameObject muzzleFX = Instantiate<GameObject>(_weaponM._muzzleFirePrefab,
                            _muzzleTransform.position,
                            Quaternion.identity);
                        // Destroy(muzzleFX, 0.5f);

                        _audioSource.pitch = Random.Range(0.9f, 1.1f);
                        bulletsInMagazine--;
                        OnPlayerBulletsValueChanged();

                        _audioSource.clip = _weaponM.fireAudio;
                        _audioSource.Play();
                        //_audioSource.PlayOneShot(_weaponM.fireAudio);
                    }
                    else {
                        ReloadWeapon();
                    }
                }
            }
        }

        private float GetFireRateFinal() {
            return fireRateFinal = _weaponM.fireRate;
        }
        protected  Vector3 FireDirectVector() {
            return new Vector3(SimpleInput.GetAxis("HorizontalRight"), 0, SimpleInput.GetAxis("VerticalRight"));
        }

        public void ReloadWeapon() {
            StartCoroutine(Reload());
        }

        protected IEnumerator Reload() {
            canFireFinal = false;

            _audioSource.clip = _weaponM.reloadAudio;
            _audioSource.Play();
            if (bulletsInInventory >= _weaponM.bulletsPerMagazine) {
                bulletsInMagazine = _weaponM.bulletsPerMagazine;
                bulletsInInventory -= _weaponM.bulletsPerMagazine;
            }
            else {
                bulletsInMagazine = bulletsInInventory;
                bulletsInInventory = 0;
            }
            OnPlayerBulletsValueChanged();

            yield return new WaitForSeconds(GetTimeToReloadFinal());

            canFireFinal = true;
        }

        private float GetTimeToReloadFinal() {
            return timeToReloadFinal = _weaponM.timeToReload;
        }

        //Called from WeaponManager
        public void ActivateWeapon(bool activate) {
            StopAllCoroutines();
            _weaponM.canFire = true;
            gameObject.SetActive(activate);
            OnPlayerBulletsValueChanged();
            ReloadWeapon();
        }

        private void OnPlayerBulletsValueChanged() { //обновление бара патронов
            foreach (var item in _bulletChangedUIArray) {
                item.OnPlayerBulletsValueChanged(bulletsInMagazine, bulletsInInventory, _weaponM.bulletsPerMagazine);

            }
        }
    }
}