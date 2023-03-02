

using UnityEngine;

namespace Assets.Scripts.Units.Enemy.Data {
    [CreateAssetMenu(fileName = "Data", menuName = "Spawner/EnemySpawner")]
    public class EnemySpawnerModelData : ScriptableObject {
        public GameObject enemyPrefab;
        public Transform[] spawnPoints;
        public float spawnInterval = 0.5f;
        public int enemiesPerWave = 100;
        public float nextSpawnTime = 5f;
        public int waveNumber = 3;
        public float newWaveTimer = 5f;

    }
}
