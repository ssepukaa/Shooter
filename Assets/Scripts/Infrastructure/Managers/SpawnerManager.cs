using Assets.Scripts.Units.Enemy;
using Assets.Scripts.Units.Enemy.Data;
using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers {
    public class SpawnerManager : MonoBehaviour, IPlayerDead {

        //public EnemySpawnerModelData enemySpawnerModelsList;

        private Player _player;
        private GameManager _gameManager;
        private bool _isPlayerDead = false;

        public void Init(GameManager gameManager, Player player) {
            _gameManager = gameManager;
            _player = player;

        }

        public void Spawn(EnemySpawnerModelData enemySpawnerModel) {
            var spawner = gameObject.AddComponent<EnemySpawner>();
            spawner.Initialize(_player, enemySpawnerModel, this);
        }

        public void OnPlayerDead() {
            _isPlayerDead = true;
        }
    }
}
