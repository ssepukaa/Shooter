using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Units.Players;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Units.Enemy {
    public class EnemySpawner : MonoBehaviour {
        public SoundManager soundManager;
        public GameObject enemyPrefab;
        public Player player;

        [HideInInspector] public Transform _plTransform;

        //public Texture crosshairTexture;
        public float spawnInterval = 2; //Spawn new enemy each n seconds
        public int enemiesPerWave = 5; //How many enemies per wave
        public Transform[] spawnPoints;

        float nextSpawnTime = 0;
        int waveNumber = 1;
        bool waitingForWave = true;
        float newWaveTimer = 0;

        int enemiesToEliminate;

        //How many enemies we already eliminated in the current wave
        int enemiesEliminated = 0;
        int totalEnemiesSpawned = 0;

        [SerializeField] private bool _isPlayerDeath = false;

        // Start is called before the first frame update
        void Start() {
            player = FindObjectOfType<Player>();
            _plTransform = player.transform;

            //Lock cursor
            // Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;

            //Wait 10 seconds for new wave to start
            newWaveTimer = 2;
            waitingForWave = true;
        }

        // Update is called once per frame
        void Update() {
            if (!_isPlayerDeath) {
                if (waitingForWave) {
                    if (newWaveTimer >= 0) {
                        newWaveTimer -= Time.deltaTime;
                    }
                    else {
                        //Initialize new wave
                        enemiesToEliminate = waveNumber * enemiesPerWave;
                        enemiesEliminated = 0;
                        totalEnemiesSpawned = 0;
                        waitingForWave = false;
                    }
                }
                else {
                    if (Time.time > nextSpawnTime) {
                        nextSpawnTime = Time.time + spawnInterval;

                        //Spawn enemy 
                        if (totalEnemiesSpawned < enemiesToEliminate && !_isPlayerDeath) {
                            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                            GameObject enemy = Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
                            //NPCEnemy npc = enemy.GetComponent<NPCEnemy>();
                            //npc.playerTransform = player.transform;
                            // npc.es = this;
                            totalEnemiesSpawned++;
                        }
                    }
                }
            }


            if (player.GetHealth() <= 0) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
            }
        }

        void OnGUI() {
            GUI.Box(new Rect(10, Screen.height - 35, 100, 25), ((int)player.GetHealth()).ToString() + " HP");
            //GUI.Box(new Rect(Screen.width / 2 - 35, Screen.height - 35, 70, 25), player.weaponManager.selectedWeapon.bulletsPerMagazine.ToString());

            if (player.GetHealth() <= 0) {
                GUI.Box(new Rect(Screen.width / 2 - 85, Screen.height / 2 - 20, 170, 40),
                    "Game Over\n(Press 'Space' to Restart)");
            }
            else {
                // GUI.DrawTexture(new Rect(Screen.width / 2 - 3, Screen.height / 2 - 3, 6, 6), crosshairTexture);
            }

            GUI.Box(new Rect(Screen.width / 2 - 50, 10, 100, 25), (enemiesToEliminate - enemiesEliminated).ToString());

            if (waitingForWave) {
                GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height / 4 - 12, 250, 25),
                    "Waiting for Wave " + waveNumber.ToString() + " (" + ((int)newWaveTimer).ToString() +
                    " seconds left...)");
            }
        }

        public void EnemyEliminated(NPCEnemy enemy) {
            enemiesEliminated++;

            if (enemiesToEliminate - enemiesEliminated <= 0) {
                //Start next wave
                newWaveTimer = 10;
                waitingForWave = true;
                waveNumber++;
            }
        }

        public void PlayerDeath() {
            _isPlayerDeath = true;
        }
    }
}