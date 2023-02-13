using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Services;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Units.Players
{
    public class Player : Unit, IEntity, IPlayer
    {
        //public WeaponListData currWeaponListData;

        public Transform _muzzle;
        private MoveService moveService;
        private Weapon _selectedWeapon;
        
        private GameManager _gameManager;

        [Header("BaseStats")]
        public float health = 100f;
        public float movementSpeed = 4f;
        public float rotationSpeed = 1f;
        public float jumpSpeed = 1.0F;
        public bool canMove = true;

        //[Header("UniqClassStats")]
        


        private void Start()
        {
           
            
            _selectedWeapon = GetComponent<Weapon>();
            moveService = new MoveService(this.transform);
            _gameManager = FindObjectOfType<GameManager>();
          

        }

        private void Update()
        {
            moveService.Move();
        }
        

        


        public float GetHealthRef()
        {
            return health;
        }

        public float GetMovementSpeedRef()
        {
            return movementSpeed;
        }

        public float GetRotationSpeedRef()
        {
            return rotationSpeed;
        }

        public float GetJumpSpeedRef()
        {
            return jumpSpeed;
        }

        public bool GetCanMovRef()
        {
            return canMove;
        }
        public void FireWeapon()
        {
           _selectedWeapon.FireWeapon();
        }

        public void MessageAfterDeath()
        {
           
            _gameManager.PlayerDeathMessage();
        }

        public Transform GetMuzzleTransform()
        {
            return _muzzle;
        }
        public float GetMoveSpeed()
        {
            return movementSpeed;
        }
        public void SetMoveSpeed(float value)
        {
           movementSpeed = value;
        }

        public float GetJumpSpeed()
        {
            return jumpSpeed;
        }

        public float GetHealth()
        {
            return health;
        }
        public void ApplyDamage(float damageAmount)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                //Player is dead
                canMove = false;
                health = 0;
                Death();
            }
        }

        protected void Death()
        {
            MessageAfterDeath();
            Destroy(gameObject);

        }
   
        public void Hit(Vector3 position, Quaternion rotation)
        {

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
    }
}
