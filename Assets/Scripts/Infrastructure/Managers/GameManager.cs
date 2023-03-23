using System.Collections;
using Assets.Scripts.UI.Menu;

using Assets.Scripts.Units.Enemy.Data;

using Assets.Scripts.Units.Pickups.Data;
using Assets.Scripts.Units.Players;
using Assets.Scripts.Weapons;
using UnityEngine;




namespace Assets.Scripts.Infrastructure.Managers {
    public class GameManager : MonoBehaviour, IPlayerDead {

        [Header("RandomPickup")]
        public TypeRandomPickupGroup typeRandomPickupGroup; // надо добавить разных типов
        public EnemySpawnerModelData[] enemySpawnerModelsList; // скриптблобжекты для моделей спавнера врагов
        [SerializeField] private AudioClip[] audioClips;
        private RandomPickupModelData _randomPickupModelData;

        [Header("PlayerReference")]
        private Player _player;

        [Header("SpawnManagerReference")]
        private SpawnerManager _spawnManager;

        private bool _isPlayerDead = false;
        [Header("UIReference")]
        private UIInterfaceButtonsPopups _uiInterfaceButtonsPopups;
        private bool _isOpenedPopupNewLevel = false; //тестово на одно открытие окна

        [Header("Timer")]
        private Stopwatch _stopwatch;
        [Header("AudioManagerReference")]
        private AudioSource AudioSource;



        private void Start() {
            
            AudioSource = GetComponent<AudioSource>();
            AudioSource.clip=audioClips[Random.Range(0, audioClips.Length)];
            AudioSource.Play();

            _player = FindObjectOfType<Player>();

            _spawnManager = GetComponent<SpawnerManager>();
            _spawnManager.Init(this, _player);
            _spawnManager.Spawn(enemySpawnerModelsList[0]);
            _uiInterfaceButtonsPopups = FindObjectOfType<UIInterfaceButtonsPopups>();
            _stopwatch = GetComponent<Stopwatch>();
            _stopwatch.StartStopwatch();
            
        }


        public void OnPlayerDead() {
            _isPlayerDead = true;

            StartCoroutine(DelayBeforeDead());

            _uiInterfaceButtonsPopups.GameOver();
        }

        IEnumerator DelayBeforeDead() {
            yield return new WaitForSeconds(2f);
            Time.timeScale = 0;
        }

        public void OnLevelUpOpenPopup(WeaponC weaponC) {
            if (_isOpenedPopupNewLevel) return; //тестово на одно открытие окна
            Debug.Log("Завершить систему прокачки каждый уровень, чтобы не было ошибок");
            _uiInterfaceButtonsPopups.OpenPopupNewLevel(weaponC, this);
            _isOpenedPopupNewLevel = true;
        }

        // public void OnLevelUpClosePopup() {
        //
        // }

        public void GetNewWeaponForPlayer(WeaponC weaponC) {
            _player.GetNewWeaponForWeapon(weaponC);
        }

        public PickupModelData GetRandomModelData() {
            // добавить систему рандомизации разных групп пикапов!!!!
            //_______________________________________________________________________________
            _randomPickupModelData = typeRandomPickupGroup.randomPickupModelsData[0];
            var model = _randomPickupModelData.GetRandomModelData();
            return model;
            //____________________________________________________________________________
        }

        public void CreatePickupOnEnemyDeath(Transform transform) {
            var pref = GetRandomModelData();
            if (pref.typePickup == TypePickup.none) return;

            Instantiate(pref.prefabBullet, transform.position,
                                transform.rotation);


        }
    }
}