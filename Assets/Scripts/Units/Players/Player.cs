using System;
using System.Linq;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Services;
using Assets.Scripts.UI.Exp;
using Assets.Scripts.Units.Enemy;
using Assets.Scripts.Units.Players.Data;
using Assets.Scripts.Weapons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;


namespace Assets.Scripts.Units.Players {
    public class Player : Unit, IPlayer, IEntity, IEnemyDead {

        public event Action<float> OnPlayerHealthValueChangedEvent;


        public PlayerModelData playerModelData;


        //public WeaponListData currWeaponListData;
        private SoundManager _soundManager;
        public Transform _muzzle;
        private MoveService moveService;
        private Weapon _selectedWeapon;

        private GameManager _gameManager;


        [Header("BaseStats")]
        [SerializeField] private float health = 100f;

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float _exp = 0;
        private int _enemyDeadCount = 0;
        private float _levelExp;
        private int _level = 1;


        [Header("Movement")]
        public float movementSpeed = 4f;

        public float rotationSpeed = 1f;
        public float jumpSpeed = 1.0F;
        public bool canMove = true;


        [Header("VFX/SFX")]
        private Vector3 _hitPosition;

        private Quaternion _hitRotation;

        [Header("UI")] private IExpUI[] _expUiList;


        // [Header("Weapon")] 

        // [SerializeField] private float fireRatePlayer;

        // [SerializeField] private float timeToReloadPlayer;

        // [SerializeField] private float weaponDamagePlayer;

        // [SerializeField] private float bulletSpeedPlayer;

        // [SerializeField] private bool canFirePlayer;


        //[Header("UniqClassStats")]


        private void Start() {

            health = maxHealth;
            _soundManager = FindObjectOfType<SoundManager>();

            _selectedWeapon = GetComponent<Weapon>();
            moveService = new MoveService(this.transform);
            _gameManager = FindObjectOfType<GameManager>();
            this.OnPlayerHealthValueChangedEvent?.Invoke(this.HealthPercent());
            _expUiList = FindObjectsOfType<MonoBehaviour>().OfType<IExpUI>().ToArray();
            OnExpChanged(_exp);
        }

        private void Update() {
            moveService.Move();
        }

        public float HealthPercent() {
            return this.health / this.maxHealth;
        }

        public void FireWeapon() {
            _selectedWeapon.FireWeapon();
        }



        public Transform GetMuzzleTransform() {
            return _muzzle;
        }

        public float GetMoveSpeed() {
            return movementSpeed;
        }

        public void SetMoveSpeed(float value) {
            movementSpeed = value;
        }


        public void ApplyDamage(float damageAmount) {
            health -= damageAmount;
            _soundManager.PlaySound(playerModelData.GetSoundOfHit(), 0.3f, 1f);



            if (health <= 0) {
                //Player is dead
                canMove = false;
                health = 0;
                Death();
            }
            this.OnPlayerHealthValueChangedEvent?.Invoke(this.HealthPercent());

        }

        public void Hit(Vector3 position, Quaternion rotation) {
            Instantiate(playerModelData.onHitPrefab, position, rotation);
            _hitPosition = position;
            _hitRotation = rotation;
        }



        protected void Death() {

            _soundManager.PlaySound(playerModelData.GetSoundOfDeath(), 0.3f, 1f);
            Instantiate(playerModelData.onDeadPrefab, _hitPosition, _hitRotation);
            movementSpeed = 0;
            moveService.canMove = false;

            var recieversOnDeathList = FindObjectsOfType<Object>().
                OfType<IPlayerDead>().ToArray();
            foreach (var item in recieversOnDeathList) {
                item.OnPlayerDead();

            }

            Destroy(gameObject, 2f);

        }

        /*
       private void GetNextWeapon()
       {
           int count = _weaponList.Count;
           int indexCurrentWeapon = _weaponList.IndexOf(_selectedWeapon);
           if ((indexCurrentWeapon + 1) < (count - 1))
           {
               _selectedWeapon = _weaponList[indexCurrentWeapon + 1];
           }

           if ((indexCurrentWeapon + 1) > (count - 1))
           {
               _selectedWeapon = _weaponList[0];
           }
       }


       private void GetPreviewWeapon()
       {
           int count = _weaponList.Count;
           int indexCurrentWeapon = _weaponList.IndexOf(_selectedWeapon);
           if ((indexCurrentWeapon - 1) >= 0)
           {
               _selectedWeapon = _weaponList[indexCurrentWeapon - 1];
           }

           if ((indexCurrentWeapon - 1) < 0)
           {
               _selectedWeapon = _weaponList[count - 1];
           }

       }
       */

        public float GetHealth() {
            return health;
        }

        public float GetJumpSpeed() {
            return jumpSpeed;
        }

        public void OnEnemyDead(float exp) {
            OnExpChanged(exp);
            _enemyDeadCount++;

        }

        public void OnExpChanged(float exp) {
            _exp += exp;
            if (_exp >= GetLevelExp()) {
                var expTemp = _exp - GetLevelExp();
                _exp = expTemp;
                _level++;
            }
            foreach (var item in _expUiList) {
                item.OnPlayerExpValueChanged(_exp,
                    GetLevelExp(),
                    _enemyDeadCount,
                    _level);
            }
        }

        private float GetLevelExp() {
            _levelExp = (float)_level * 10f;
            return _levelExp;
        }
    }
}