using System.Collections.Generic;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerC : Unit, IWeaponHolder
    {
        public PlayerM playerM;

        private PlayerV playerV;
        private MoveService moveService;

        private WeaponC _selectedWeaponC;
        private WeaponService weaponService;
        public WeaponListData _currWeaponListData;
        private List<WeaponC> weaponList;
        [HideInInspector]
        public bool canMove = true;




        private void Start()
        {
            _selectedWeaponC = gameObject.GetComponentInChildren<WeaponC>();
            moveService = new MoveService(this.transform);
            playerV = new PlayerV();

        }

        private void Update()
        {
            moveService.Move();
        }

        public void FireWeapon()
        {
            _selectedWeaponC.FireWeapon();
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
