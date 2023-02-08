using System.Collections.Generic;
using Assets.Scripts.Player;
using Assets.Scripts.Services;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers
{
    public class WeaponManager: MonoBehaviour
    {
        
        [SerializeField] private WeaponC selectedWeaponC;
        private GameManager _gameManager;
        private WeaponService _weaponService;
        public WeaponListData _weaponListData;
        private PlayerC _playerC;

        void Awake()
        {

        }
        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            
            DontDestroyOnLoad(this);
        }

        
        // private void CreateWeaponService()
        // {
        //     _weaponService = new WeaponService(this, _weaponListData);
        // }
      
    }
}


    
    