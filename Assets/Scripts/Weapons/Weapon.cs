using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.UI.WeaponUI;
using Assets.Scripts.Units.Pickups.Data;
using Assets.Scripts.Units.Players;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;
using Random = UnityEngine.Random;


namespace Assets.Scripts.Weapons {
    public enum BulletsTypes {
        noneBullets = 0,
        bulletsRifle = 1,
        bulletsShotgun = 2,
        bulletsSniperRifle = 3,
        bulletsMinigun = 4,
        bulletsRPG = 5,
        bulletsFlamethrower = 6,

    }
    //[RequireComponent(typeof(AudioSource))]
    public class Weapon : Actor {
        [SerializeField] private WeaponM _rifleWeaponM;
        [SerializeField] private WeaponM _shotgunWeaponM;
        [SerializeField] private WeaponM _sniperRifleWeaponM;
        [SerializeField] private WeaponM _minigunWeaponM;
        [SerializeField] private WeaponM _rpgWeaponM;
        [SerializeField] private WeaponM _flamethrowerWeaponM;
        [SerializeField] private WeaponM _katanaWeaponM;
        private WeaponC _rifleWeaponC;
        private WeaponC _shotgunWeaponC;
        private WeaponC _sniperRifleWeaponC;
        private WeaponC _minigunWeaponC;
        private WeaponC _rpgWeaponC;
        private WeaponC _flamethrowerWeaponC;
        private WeaponC _katanaWeaponC;

        private WeaponC _currentWeaponC;
        private WeaponM _currentWeaponM;
        private int _countWeaponC = 0;
        // private int _indexCurrentWeaponC = 0;

        // [SerializeField] List<WeaponM> _weaponMArray;
        //________________________________________________________________________
        private BulletsTypes _bulletsTypes;
        private TypePickup _typePickup;
        private Dictionary<BulletsTypes, int> _bulletsDictionary = new Dictionary<BulletsTypes, int>(7);
        private Dictionary<TypePickup, int> _pickupsDictionary = new Dictionary<TypePickup, int>();
        private WeaponC[] _weaponCHandleArray = new WeaponC[5];
        private WeaponC[] _weaponCAllArray = new WeaponC[7];
        private WeaponM[] _weaponMArray = new WeaponM[7];
        private GameObject[] _weaponObjectsUIArray = new GameObject[5];
        //_________________________________________________________________________
        // protected WeaponManager _weaponManager;
        protected Transform _muzzleTransform;
        private float _scatter;
        private AudioSource _audioSource;
        private IBulletChangedUI[] _bulletChangedUIArray;
        private UIWidgetWeaponBar _weaponBarUI;

        private Player _player;
        //private float _minScatter = 0.1f;
        // private float _maxScatter = 0.2f;
        [Header("Weapon")]
        [SerializeField] private int _bulletsTypeRifle = 300;
        [SerializeField] private int _bulletsTypeRPG = 10;
        [SerializeField] private int _bulletsTypeShotgun = 100;
        [SerializeField] private int _bulletsTypeSniperRifle = 30;
        [SerializeField] private int _bulletsTypeMinigun = 10;
        [SerializeField] private int _bulletsTypeFlamethrower = 20;
        // [SerializeField] private int _bulletsTypeRPG=10;

        [SerializeField] private float fireRateFinal;
        [SerializeField] private float timeToReloadFinal;
        [SerializeField] private float weaponDamageFinal;
        [SerializeField] private float bulletSpeedFinal;
        [SerializeField] private bool canFireFinal = true;
        [SerializeField] private float nextFireTime = 0;
        [SerializeField] private int _bulletsInMagazine;
        // [SerializeField] private int _bulletsInInventory;






        void Start() {
            _weaponMArray[0] = _rifleWeaponM;
            _weaponMArray[1] = _shotgunWeaponM;
            _weaponMArray[2] = _sniperRifleWeaponM;
            _weaponMArray[3] = _minigunWeaponM;
            _weaponMArray[4] = _rpgWeaponM;
            _weaponMArray[5] = _flamethrowerWeaponM;
            _weaponMArray[6] = _katanaWeaponM;
            _pickupsDictionary[TypePickup.bulletsRPG] = _bulletsTypeRPG;
            _pickupsDictionary[TypePickup.bulletsRifle] = _bulletsTypeRifle;
            _pickupsDictionary[TypePickup.bulletsShotgun] = _bulletsTypeShotgun;
            _pickupsDictionary[TypePickup.bulletsSniperRifle] = _bulletsTypeSniperRifle;
            _pickupsDictionary[TypePickup.bulletsMinigun] = _bulletsTypeMinigun;
            _pickupsDictionary[TypePickup.bulletsFlamethrower] = _bulletsTypeFlamethrower;
            _pickupsDictionary[TypePickup.none] = 0;

            _player = GetComponent<Player>();

            _muzzleTransform = _player.GetMuzzleTransform();
            //   _weaponManager = FindObjectOfType<WeaponManager>();

            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            //Make sound 3D
            _audioSource.spatialBlend = 1f;
            _bulletChangedUIArray = FindObjectsOfType<MonoBehaviour>().OfType<IBulletChangedUI>().ToArray();
            _weaponBarUI = FindObjectOfType<UIWidgetWeaponBar>();
            _weaponBarUI.InitStart(this);
            GetReferenceWeaponUI();
            CreateAllWeaponsAndGetFirstWeaponC();
            _weaponBarUI.SetImageWeapon(_weaponObjectsUIArray[0], _weaponCHandleArray[0].weaponM.spriteForHUD);

            _weaponBarUI.EnableIdleState(_weaponObjectsUIArray[0]);

            _currentWeaponC = _weaponCHandleArray[0];

            //_currentWeaponM = _currentWeaponC.weaponM;
            //ReloadWeapon();
            // _bulletsInInventory = _bulletsTypeRifle;

            _weaponBarUI.Button1();
        }

        private void CreateAllWeaponsAndGetFirstWeaponC() {

            _rifleWeaponC = new WeaponC(_rifleWeaponM, this, _player);
            _shotgunWeaponC = new WeaponC(_shotgunWeaponM, this, _player);
            _sniperRifleWeaponC = new WeaponC(_sniperRifleWeaponM, this, _player);
            _minigunWeaponC = new WeaponC(_minigunWeaponM, this, _player);
            _rpgWeaponC = new WeaponC(_rpgWeaponM, this, _player);
            _flamethrowerWeaponC = new WeaponC(_flamethrowerWeaponM, this, _player);
            _katanaWeaponC = new WeaponC(_katanaWeaponM, this, _player);

            _weaponCAllArray[0] = _rifleWeaponC;
            _weaponCAllArray[1] = _shotgunWeaponC;
            _weaponCAllArray[2] = _sniperRifleWeaponC;
            _weaponCAllArray[3] = _minigunWeaponC;
            _weaponCAllArray[4] = _rpgWeaponC;
            _weaponCAllArray[5] = _flamethrowerWeaponC;
            _weaponCAllArray[6] = _katanaWeaponC;

            _weaponCHandleArray[_countWeaponC] = _weaponCAllArray[0];
            //_countWeaponC++;
            _weaponCAllArray[0] = null;
        }


        public void GetNewWeapon(WeaponC weaponC) {
            _countWeaponC++;
            if (_countWeaponC>=4) return; //всего 5 слотов в HUD для оружия

            var index = Array.IndexOf(_weaponCAllArray, weaponC);
            _weaponCHandleArray[_countWeaponC] = _weaponCAllArray[index];
            _weaponCAllArray[index] = null;

            ActivateWeapon(_countWeaponC+1);

            _weaponBarUI.SetImageWeapon(_weaponObjectsUIArray[_countWeaponC], _currentWeaponC.weaponM.spriteForHUD);
            _weaponBarUI.EnableIdleState(_weaponObjectsUIArray[_countWeaponC]);
            _weaponBarUI.DisableHolderState();
            _weaponBarUI.EnableHolderState(_weaponObjectsUIArray[_countWeaponC]);
            //_weaponBarUI.Button2();

        }

        public WeaponC RandomChooseNewWeapon() {
            // List<WeaponC> weaponsLastList = new List<WeaponC>();
            // foreach (var item in _weaponCAllArray) {
            //     if (item != null) {
            //         weaponsLastList.Add(item);
            //     }
            // }
            //вернуть рандомные значения, когда все оружие будет прописано
            //return weaponsLastList[Random.Range(0, weaponsLastList.Count)];
            return _weaponCAllArray[4];

        }

        void Update() {

            if (Input.GetKeyDown(KeyCode.R) && canFireFinal) {
                ReloadWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                _weaponBarUI.Button1(); ;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                _weaponBarUI.Button2(); ;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                _weaponBarUI.Button3();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) {
                _weaponBarUI.Button4();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5)) {
                _weaponBarUI.Button5();
            }
            if (Input.GetKeyDown(KeyCode.Y)) {
                ActivateAltWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.U)) {
                ActivateAltWeapon(2);
            }
            if (Input.GetKeyDown(KeyCode.I)) {
                ActivateAltWeapon(3);
            }
            if (Input.GetKeyDown(KeyCode.O)) {
                ActivateAltWeapon(4);
            }
            if (Input.GetKeyDown(KeyCode.P)) {
                ActivateAltWeapon(5);
            }

            nextFireTime += Time.deltaTime;
        }

        public void GetReferenceWeaponUI() {
            for (int i = 0; i < _weaponObjectsUIArray.Length; i++) {

                _weaponObjectsUIArray[i] = _weaponBarUI.GetReferenceItem();
            }
        }

        public void ActivateWeapon(int numWeapon) {
            if (numWeapon>_countWeaponC+1) return;
            _pickupsDictionary[_currentWeaponC.weaponM.pickupTypes] += _bulletsInMagazine;
            _bulletsInMagazine = 0;

            //------------------------------------------------------------------------------
            //Исправить на данную строку, когда будут реализованы все виды оружия кроме RPG
            //_currentWeaponC = _weaponCHandleArray[numWeapon - 1];
            _currentWeaponC = _weaponCHandleArray[numWeapon-1];


            //--------------------------------------------------------------------------------
            // _currentWeaponM = _weaponCHandleArray[numWeapon-1].weaponM;
            // switch (_currentWeaponM.bulletsTypes) {
            //     case BulletsTypes.bulletsRifle:
            //         _bulletsInInventory = _bulletsTypeRifle;
            //         break;
            //     case BulletsTypes.bulletsRPG:
            //         _bulletsInInventory = _bulletsTypeRPG;
            //         break;
            //     default:
            //         break;
            // }

            ReloadWeapon();
        }

        public void ActivateAltWeapon(int numAltWeapon) {

        }

        public void FireWeapon() {
            Fire();
        }

        protected void Fire() {
            if (canFireFinal) {
                if (nextFireTime > GetFireRateFinal()) {
                    nextFireTime = 0;

                    if (_bulletsInMagazine > 0) {
                        //Point fire point at the current center of Camera

                        Vector3 fireVector = FireDirectVector();

                        // Разброс пуль
                        _scatter = _currentWeaponC.weaponM.scatter;
                        Vector3 randomAngle = fireVector;
                        randomAngle.z += Random.Range(-_scatter, _scatter);

                        //Fire
                        _bulletsInMagazine--;
                        OnPlayerBulletsValueChanged();
                        if (_bulletsInMagazine <= 0) {
                            ReloadWeapon();
                        }

                        GameObject newBullet = Instantiate<GameObject>(
                            _currentWeaponC.weaponM.bulletPrefab,
                            _muzzleTransform.position,
                            Quaternion.identity);


                        var angleRandomForward = Quaternion.Euler(0, 0, randomAngle.z);
                        var bulletRigidbody = newBullet.GetComponent<Rigidbody>();

                        bulletRigidbody.velocity = angleRandomForward * fireVector * _currentWeaponC.weaponM.bulletSpeed;


                        //Set bullet damage according to weapon damage valuePickup
                        newBullet.GetComponent<IBullet>().SetDamage(_currentWeaponC.weaponM.weaponDamage);

                        GameObject muzzleFX = Instantiate<GameObject>(_currentWeaponC.weaponM._muzzleFirePrefab,
                            _muzzleTransform.position,
                            Quaternion.identity);
                        // Destroy(muzzleFX, 0.5f);

                        _audioSource.pitch = Random.Range(0.9f, 1.1f);
                        _bulletsInMagazine--;
                        OnPlayerBulletsValueChanged();

                        _audioSource.clip = _currentWeaponC.weaponM.fireAudio;
                        _audioSource.Play();
                        _audioSource.PlayOneShot(_currentWeaponC.weaponM.fireAudio);
                    } else {
                        ReloadWeapon();
                    }
                }
            }
        }

        private float GetFireRateFinal() {
            return fireRateFinal = _currentWeaponC.weaponM.fireRate;
        }
        protected Vector3 FireDirectVector() {
            return new Vector3(SimpleInput.GetAxis("HorizontalRight"), 0, SimpleInput.GetAxis("VerticalRight"));
        }

        public void ReloadWeapon() {
            StartCoroutine(Reload());
        }

        protected IEnumerator Reload() {
            canFireFinal = false;

            _audioSource.clip = _currentWeaponC.weaponM.reloadAudio;
            _audioSource.Play();
            if (_pickupsDictionary[_currentWeaponC.weaponM.pickupTypes]+_bulletsInMagazine >= _currentWeaponC.weaponM.bulletsPerMagazine) {
                _pickupsDictionary[_currentWeaponC.weaponM.pickupTypes] += _bulletsInMagazine;
                _bulletsInMagazine = _currentWeaponC.weaponM.bulletsPerMagazine;
                _pickupsDictionary[_currentWeaponC.weaponM.pickupTypes] -= _currentWeaponC.weaponM.bulletsPerMagazine;
            } else {
                _bulletsInMagazine = _pickupsDictionary[_currentWeaponC.weaponM.pickupTypes];
                _pickupsDictionary[_currentWeaponC.weaponM.pickupTypes] = 0;
            }
            OnPlayerBulletsValueChanged();

            yield return new WaitForSeconds(GetTimeToReloadFinal());

            canFireFinal = true;
        }

        private float GetTimeToReloadFinal() {
            return timeToReloadFinal = _currentWeaponC.weaponM.timeToReload;
        }

        //Called from WeaponManager
        public void ReadyWeapon(bool activate) {
            StopAllCoroutines();
            _currentWeaponC.weaponM.canFire = true;
            gameObject.SetActive(activate);
            OnPlayerBulletsValueChanged();
            ReloadWeapon();
        }

        private void OnPlayerBulletsValueChanged() { //обновление бара патронов
            foreach (var item in _bulletChangedUIArray) {
                item.OnPlayerBulletsValueChanged(_bulletsInMagazine, _pickupsDictionary[_currentWeaponC.weaponM.pickupTypes], _currentWeaponC.weaponM.bulletsPerMagazine);

            }
        }

        public void AddAmmo(TypePickup typePickup, int ammoValue) {
            _pickupsDictionary[typePickup] += ammoValue;
            OnPlayerBulletsValueChanged();
        }
    }
}