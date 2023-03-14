using System.Collections;
using Assets.Scripts.UI.Menu;
using Assets.Scripts.Units.Enemy;
using Assets.Scripts.Units.Enemy.Data;
using Assets.Scripts.Units.Pickups;
using Assets.Scripts.Units.Pickups.Data;
using Assets.Scripts.Units.Players;
using Assets.Scripts.Weapons;
using UnityEngine;




namespace Assets.Scripts.Infrastructure.Managers {
    public class GameManager : MonoBehaviour, IPlayerDead {

        public TypeRandomPickupGroup typeRandomPickupGroup;
        private RandomPickupModelData _randomPickupModelData;
        public EnemySpawnerModelData[] enemySpawnerModelsList;
        private Player _player;
        private SpawnerManager _spawnManager;
        private bool _isPlayerDead = false;
        private UIInterfaceButtonsPopups _uiInterfaceButtonsPopups;



        private bool _isOpenedPopupNewLevel = false; //тестово на одно открытие окна

        private void Start() {
            // if (FindObjectsOfType<GameManager>().Length > 1) {
            //     Destroy(gameObject);
            // } else {
            //     DontDestroyOnLoad(this);
            //
            // }
            _player = FindObjectOfType<Player>();

            _spawnManager = GetComponent<SpawnerManager>();
            _spawnManager.Init(this, _player);
            _spawnManager.Spawn(enemySpawnerModelsList[0]);
            _uiInterfaceButtonsPopups = FindObjectOfType<UIInterfaceButtonsPopups>();
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