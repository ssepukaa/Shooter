using System.Collections;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Units.Enemy.Data;
using Assets.Scripts.Units.Players;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Units.Enemy {
    public class EnemySpawner : SpawnerManager, IPlayerDead {
        [SerializeField] private EnemySpawnerModelData _enemySpawnerModelData;
        private Player _player;
        private SpawnerManager _spawnerManager;
        public SoundManager soundManager;

        [HideInInspector] public Transform _plTransform;

        //public Texture crosshairTexture;
        private GameObject enemyPrefab;
        private float spawnInterval; //Spawn new enemy each n seconds
        private int enemiesPerWave; //How many enemies per wave
        private Transform[] spawnPoints;

        private float _nextSpawnTime;
        private int waveNumber;
        private bool _waitingForWave;
        private bool isSpawnerLoaded = false;
        private float newWaveTimer;

        //How many enemies we already eliminated in the current wave
        private int _enemiesToEliminate;
        private int enemiesEliminated;
        private int _countEnemiesSpawned;

        [SerializeField] private bool _isPlayerDeath = false;

        // Start is called before the first frame update
        void Start() {


            soundManager = FindObjectOfType<SoundManager>();

            //Wait 10 seconds for new wave to start

        }

        public void Initialize(Player player,
            EnemySpawnerModelData enemySpawnerModelData,
            SpawnerManager spawnerManager) {
            _player = player;
            _enemySpawnerModelData = enemySpawnerModelData;
            _plTransform = _player.transform;   
            _spawnerManager = spawnerManager;
            _waitingForWave = false;
            isSpawnerLoaded = true;
            if (!_isPlayerDeath) {
                StartCoroutine(DelaySpawn());
            }
        }

        
        // Update is called once per frame
        void Update() {

            
        }

        public IEnumerator DelaySpawn() {
            yield return new WaitForSeconds(_nextSpawnTime);
            _countEnemiesSpawned = 0;
            _waitingForWave = false;

            StartCoroutine(SpawnEnemy());
        }

        public IEnumerator SpawnEnemy() {
            while (_countEnemiesSpawned <= _enemySpawnerModelData.enemiesPerWave && !_isPlayerDeath) {
                CreateEnemy();
                yield return new WaitForSeconds(_enemySpawnerModelData.spawnInterval);

            }


        }

        private void CreateEnemy() {
            Transform randomPoint = 
                _enemySpawnerModelData.spawnPoints[Random.Range(0, _enemySpawnerModelData.spawnPoints.Length)];

            GameObject enemy = Instantiate(_enemySpawnerModelData.enemyPrefab,
                randomPoint.position,
                Quaternion.identity);

            _countEnemiesSpawned++;
        }

        public void OnPlayerDead() {
            _isPlayerDeath = true;
        }

        public void OnEnemyDead(float exp) {
            _player.OnEnemyDead(exp);
        }

    }
}