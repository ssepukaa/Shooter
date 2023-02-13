
using Assets.Scripts.Units.Enemy;
using Assets.Scripts.Units.Players;
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
        private Player player;
        private EnemySpawner[] _enemySpawners;


        private void Start()
        {
            
            player = FindObjectOfType<Player>();
            
            _moveManager = FindObjectOfType<MoveManager>();
            
            _weaponManager = FindObjectOfType<WeaponManager>();
            _enemySpawners = FindObjectsOfType<EnemySpawner>();



            DontDestroyOnLoad(this);

        }

        public void PlayerDeathMessage()
        {
            foreach (EnemySpawner enemySpawner in _enemySpawners)
            {
                enemySpawner.PlayerDeath();
            }
        }
    }
}
