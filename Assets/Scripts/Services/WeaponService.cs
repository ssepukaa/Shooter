using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Weapon;
using UnityEngine;
using System.Threading;
using System.Linq;
using Assets.Scripts.Player;
using Unity.VisualScripting;

namespace Assets.Scripts.Services
{
    public class WeaponService : MyServices
    {
        // private WeaponManager _weaponManager;
        // private WeaponListData _weaponListData;
        // private List<WeaponM> _weaponList;
        // private WeaponC _weaponC;
        // private GameObject _playerGameObject;
        // private PlayerC _playerC;
        //
        // private WeaponM _selectedWeapon;
        // //
        //
        //
        // public WeaponService(WeaponManager weaponManager, WeaponListData weaponModelListData, GameObject playerGameObject, PlayerC playerC)
        // {
        //     _weaponManager = weaponManager;
        //     _weaponListData = weaponModelListData;
        //     _playerGameObject = playerGameObject;
        //     _playerC = playerC;
        //
        //     Initialize();
        //     
        //
        // }
        //
        // private void Initialize()
        // {
        //     _weaponList = _weaponListData.weaponModelListData;
        //     _selectedWeapon = _weaponList[0];
        // }
        //
        // private void Update()
        // {
        //     //Select secondary weapon when pressing 1
        //     if (SimpleInput.GetKeyDown(KeyCode.Alpha1))
        //     {
        //         GetNextWeapon();
        //        
        //     }
        //
        //     //Select primary weapon when pressing 2
        //     if (SimpleInput.GetKeyDown(KeyCode.Alpha2))
        //     {
        //         GetPreviewWeapon();
        //
        //         // primaryWeaponC.ActivateWeapon(true);
        //         // secondaryWeaponC.ActivateWeapon(false);
        //         // selectedWeaponC = primaryWeaponC;
        //     }
        // }
        //
        // private void GetNextWeapon()
        // {
        //     int count = _weaponList.Count;
        //     int indexCurrentWeapon = _weaponList.IndexOf(_selectedWeapon);
        //     if ((indexCurrentWeapon + 1) < (count-1))
        //     {
        //         _selectedWeapon = _weaponList[indexCurrentWeapon + 1];
        //     }
        //
        //     if ((indexCurrentWeapon + 1) > (count - 1))
        //     {
        //         _selectedWeapon = _weaponList[0];
        //     }
        // }
        // private void GetPreviewWeapon()
        // {
        //     int count = _weaponList.Count;
        //     int indexCurrentWeapon = _weaponList.IndexOf(_selectedWeapon);
        //     if ((indexCurrentWeapon - 1) >= 0)
        //     {
        //         _selectedWeapon = _weaponList[indexCurrentWeapon - 1];
        //     }
        //
        //     if ((indexCurrentWeapon - 1) < 0)
        //     {
        //         _selectedWeapon = _weaponList[count-1];
        //     }
        //}

    }
}
