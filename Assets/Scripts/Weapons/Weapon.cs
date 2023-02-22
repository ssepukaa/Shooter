using System.Collections;
using System.Security.Cryptography;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Weapons {
    //[RequireComponent(typeof(AudioSource))]
    public class Weapon : Actor {
        public WeaponM _weaponM;
        protected WeaponManager _weaponManager;
        protected Transform _muzzleTransform;
        [SerializeField] private float _scatter = 0.1f;
        private AudioSource _audioSource;

        private Player _player;
        //private float _minScatter = 0.1f;
        // private float _maxScatter = 0.2f;

        void Start() {
            _player = GetComponent<Player>();

            _muzzleTransform = _player.GetMuzzleTransform();
            _weaponManager = FindObjectOfType<WeaponManager>();

            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            //Make sound 3D
            _audioSource.spatialBlend = 1f;
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
            if (Input.GetKeyDown(KeyCode.R) && _weaponM.canFire) {
                ReloadWeapon();
            }
            // if (SimpleInput.GetAxis("HorizontalRight") > 0.9f || SimpleInput.GetAxis("HorizontalRight") < -0.9f ||
            //     SimpleInput.GetAxis("VerticalRight") > 0.9f || SimpleInput.GetAxis("VerticalRight") < -0.9f)
            // {
            //     Fire();
            //
            // }

            _weaponM.nextFireTime += Time.deltaTime;
        }

        public void FireWeapon() {
            Fire();
        }

        protected void Fire() {
            if (_weaponM.canFire) {
                if (_weaponM.nextFireTime > _weaponM.fireRate) {
                    _weaponM.nextFireTime = 0;

                    if (_weaponM.bulletsPerMagazine > 0) {
                        //Point fire point at the current center of Camera

                        Vector3 fireVector = FireDirectVector();

                        // Разброс пуль

                        Vector3 randomAngle = fireVector;
                        randomAngle.z += Random.Range(-_scatter, _scatter);

                        //Fire
                        GameObject newBullet = Instantiate<GameObject>(
                            _weaponM.bulletPrefab,
                            _muzzleTransform.position,
                            Quaternion.identity);

                        // GameObject newBullet = Instantiate<GameObject>(_weaponM.bulletPrefab, _muzzleTransform.transform.position, 
                        var angleRandomForward = Quaternion.Euler(0, 0, randomAngle.z);
                        var bulletRigidbody = newBullet.GetComponent<Rigidbody>();
                        //var scatteredVector = fireVector;
                        //scatteredVector = Quaternion.Euler(0, 10, 0) * scatteredVector;
                        //Vector3 rotationVector = new Vector3(30, 30, 0);
                        // fireVector = ;
                        bulletRigidbody.velocity = angleRandomForward * fireVector * _weaponM.bulletSpeed;
                        //bulletRigidbody.velocity = newBullet.transform.forward.normalized * _weaponM.bulletSpeed;


                        //Set bullet damage according to weapon damage value
                        newBullet.GetComponent<Bullet>().SetDamage(_weaponM.weaponDamage);

                        GameObject muzzleFX = Instantiate<GameObject>(_weaponM._muzzleFirePrefab,
                            _muzzleTransform.position,
                            Quaternion.identity);
                        // Destroy(muzzleFX, 0.5f);

                        _audioSource.pitch = Random.Range(0.9f, 1.1f);
                        _weaponM.bulletsPerMagazine--;
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

        protected  Vector3 FireDirectVector() {
            return new Vector3(SimpleInput.GetAxis("HorizontalRight"), 0, SimpleInput.GetAxis("VerticalRight"));
        }

        public void ReloadWeapon() {
            StartCoroutine(Reload());
        }

        protected IEnumerator Reload() {
            _weaponM.canFire = false;

            _audioSource.clip = _weaponM.reloadAudio;
            _audioSource.Play();

            yield return new WaitForSeconds(_weaponM.timeToReload);

            //_weaponM.bulletsPerMagazine = _weaponM.bulletsPerMagazine;

            _weaponM.canFire = true;
        }

        //Called from WeaponManager
        public void ActivateWeapon(bool activate) {
            StopAllCoroutines();
            _weaponM.canFire = true;
            gameObject.SetActive(activate);
        }
    }
}