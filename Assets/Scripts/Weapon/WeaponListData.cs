
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "Data", menuName = "Weapon/WeaponList", order = 3)]
    public class WeaponListData: ScriptableObject
    {
        public List<WeaponM> weaponModelListData;

        public List<WeaponC> weaponControllerListData;
    }
}
