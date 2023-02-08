using System.Collections;
using Assets.Scripts.Infrastructure.Managers;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponC : MonoBehaviour
    {
        //public Transform firePoint;

        private WeaponManager _weaponManager;

        AudioSource _audioSource;
        public WeaponM _weaponM;
        private Transform _muzzle;
       


        void Start()
        {
            
            _weaponManager = FindObjectOfType <WeaponManager>();
            _muzzle = FindObjectOfType<Muzzle>().transform;
            
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
        void Fire()
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
                        GameObject newBullet = Instantiate(_weaponM.bulletPrefab, _muzzle.transform.position, _muzzle.transform.rotation);
                        var bulletRigidbody = newBullet.GetComponent<Rigidbody>();
                        bulletRigidbody.velocity = fireVector * _weaponM.bulletSpeed;


                        //Set bullet damage according to weapon damage value
                        newBullet.GetComponent<Bullet>().SetDamage(_weaponM.weaponDamage);

                        _audioSource.pitch = Random.Range(0.8f, 1.2f);
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

        private Vector3 FireDirectVector()
        {
            return new Vector3(SimpleInput.GetAxis("HorizontalRight"), 0, SimpleInput.GetAxis("VerticalRight"));
        }

        public void ReloadWeapon()
        {
            StartCoroutine(Reload());
        }
        IEnumerator Reload()
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

