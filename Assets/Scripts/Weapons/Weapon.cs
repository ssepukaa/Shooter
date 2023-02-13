using System.Collections;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [RequireComponent(typeof(AudioSource))]
    public class Weapon: Actor
    {
        protected WeaponManager _weaponManager;

        private AudioSource _audioSource;
        public WeaponM _weaponM;
        protected Transform _muzzleTransform;
        private Player _player;

        void Start()
        {
            _player = GetComponent<Player>();
            
            _muzzleTransform = _player.GetMuzzleTransform();
            _weaponManager = FindObjectOfType<WeaponManager>();

            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            //Make sound 3D
            _audioSource.spatialBlend = 1f;

        }
        void Update()
        {

            // if (Input.GetMouseButtonDown(0) && _weaponM.singleFire)
            // {
            //     Fire();
            // }
            // if (Input.GetMouseButton(0) && !_weaponM.singleFire)
            // {
            //     Fire();
            // }
            if (Input.GetKeyDown(KeyCode.R) && _weaponM.canFire)
            {
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

        public void FireWeapon()
        {
            Fire();
        }
        protected void Fire()

        {

            if (_weaponM.canFire)
            {
                if (_weaponM.nextFireTime > _weaponM.fireRate)
                {
                    _weaponM.nextFireTime = 0;

                    if (_weaponM.bulletsPerMagazine > 0)
                    {

                        //Point fire point at the current center of Camera

                        Vector3 fireVector = FireDirectVector();

                        //Fire
                        GameObject newBullet = Instantiate<GameObject>(_weaponM.bulletPrefab, _muzzleTransform.transform.position, _muzzleTransform.transform.rotation);
                        var bulletRigidbody = newBullet.GetComponent<Rigidbody>();
                        bulletRigidbody.velocity = fireVector * _weaponM.bulletSpeed;


                        //Set bullet damage according to weapon damage value
                        newBullet.GetComponent<Bullet>().SetDamage(_weaponM.weaponDamage);

                        _audioSource.pitch = Random.Range(0.9f, 1.1f);
                        _weaponM.bulletsPerMagazine--;
                        _audioSource.clip = _weaponM.fireAudio;
                        _audioSource.Play();
                    }
                    else
                    {
                        ReloadWeapon();
                    }
                }
            }
        }

        protected private Vector3 FireDirectVector()
        {
            return new Vector3(SimpleInput.GetAxis("HorizontalRight"), 0, SimpleInput.GetAxis("VerticalRight"));
        }

        public void ReloadWeapon()
        {
            StartCoroutine(Reload());
        }
        protected IEnumerator Reload()
        {
            _weaponM.canFire = false;

            _audioSource.clip = _weaponM.reloadAudio;
            _audioSource.Play();

            yield return new WaitForSeconds(_weaponM.timeToReload);

            //_weaponM.bulletsPerMagazine = _weaponM.bulletsPerMagazine;

            _weaponM.canFire = true;
        }

        //Called from WeaponManager
        public void ActivateWeapon(bool activate)
        {
            StopAllCoroutines();
            _weaponM.canFire = true;
            gameObject.SetActive(activate);
        }
    }

}

