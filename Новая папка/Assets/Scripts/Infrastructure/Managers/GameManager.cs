using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameObject WeaponManagerPrefab;
        public GameObject MoveManagerPrefab;
        private GameObject _weaponManagerObject;
        private GameObject _moveManagerObject;
        private MoveManager _moveManager;
        private WeaponManager _weaponManager;
        private PlayerC _playerC;


        private void Start()
        {
            
            _playerC = FindObjectOfType<PlayerC>();
            
            _moveManager = FindObjectOfType<MoveManager>();
            
            _weaponManager = FindObjectOfType<WeaponManager>();
            

            
            DontDestroyOnLoad(this);

        }
    }
}
