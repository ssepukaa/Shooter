using System.Collections;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Units.Enemy.Data;
using Assets.Scripts.Units.Players;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.Units.Enemy {
    public class EnemySpawner : SpawnerManager, IPlayerDead {       
        
        public SoundManager soundManager;  
        [HideInInspector] public Transform _plTransform;

        [SerializeField] private EnemySpawnerModelData _enemySpawnerModelData; 
        [SerializeField] private bool _isPlayerDeath = false;
        private Player _player;
        private SpawnerManager _spawnerManager;
        
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

       

        private int[] spawnDistance = new []{-20,20}; // Distance from camera bounds where enemies will spawn
        


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
          
            // Calculate a random position outside the camera bounds
            Vector3 randomPosition = _player.transform.position + 
                                     new Vector3(spawnDistance[Random.Range(0,spawnDistance.Length)],
                                         0f,
                                         spawnDistance[Random.Range(0, spawnDistance.Length)]);

            // Find the closest point on the NavMesh to the random position
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 2f, NavMesh.AllAreas)) {
                // Instantiate the enemy prefab at the closest point on the NavMesh
                GameObject enemy = Instantiate(_enemySpawnerModelData.enemyPrefab,
                    hit.position, Quaternion.identity);

                _countEnemiesSpawned++;
                
            }
            


        }

        public void OnPlayerDead() {
            _isPlayerDeath = true;
        }

        public void OnEnemyDead(float exp) {
            _player.OnEnemyDead(exp);
        }

    }
}