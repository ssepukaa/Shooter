using Assets.Scripts.Units.Players;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers
{
    public class WeaponManager: MonoBehaviour
    {
        
        [SerializeField] private Weapon selectedWeaponC;
        private GameManager _gameManager;
       // private Weapon _weaponService;
        //public Weapon _weaponListData;
        private Player player;

        void Awake()
        {

        }
        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            
            //DontDestroyOnLoad(this);
        }

        
        // private void CreateWeaponService()
        // {
        //     _weaponService = new WeaponService(this, _weaponListData);
        // }
      
    }
}


    
    