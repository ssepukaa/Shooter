using System.Collections;
using Assets.Scripts.Units.Enemy;
using Assets.Scripts.Units.Enemy.Data;
using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers {
    public class GameManager : MonoBehaviour,IPlayerDead {
        
        public EnemySpawnerModelData[] enemySpawnerModelsList;
        private Player player;
        private SpawnerManager _spawnManager;
        private bool _isPlayerDead = false;


        private void Start() {
            if (FindObjectsOfType<GameManager>().Length > 1) {
                Destroy(gameObject);
            } else {
                DontDestroyOnLoad(this);

            }
            player = FindObjectOfType<Player>();

            _spawnManager = GetComponent<SpawnerManager>();
            _spawnManager.Init(this, player);
            _spawnManager.Spawn(enemySpawnerModelsList[0]);
        }


        public void OnPlayerDead() {
            _isPlayerDead = true;

            StartCoroutine(DelayBeforeDead());

        }

        IEnumerator DelayBeforeDead() {
            yield return new WaitForSeconds(2f);
            Time.timeScale = 0;
        }
    }
}