using System.Collections;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu {
    public class UIInterfaceButtonsPopups : MonoBehaviour {
        public GameObject backgroundBeforeStart;
        public GameObject gameoverPopup;
        public GameObject uiPopupNewLevel;
        public TextMeshProUGUI nameWeapon;
        public Image iconWeapon;
        public TextMeshProUGUI levelWeapon;
        public TextMeshProUGUI describeWeapon;
        private WeaponC _gettingWeaponC;
        private GameManager _gameManager;
        


        private void Awake() {
            backgroundBeforeStart.SetActive(true);
            PauseButton();
            //StartCoroutine(DelayBeforeStart());
            backgroundBeforeStart.SetActive(false);
            UnPauseButton();
        }

        public void GameOver() {
            gameoverPopup.SetActive(true);
            PauseButton();
        }
        public void ResumeButtonClick() {

            UnPauseButton();
        }

        public void ExitButtonClick() {
            UnPauseButton();
            SceneManager.LoadScene("StartScene");
        }

        public void PauseButton() {


            Time.timeScale = 0f;
        }

        public void UnPauseButton() {
            Time.timeScale = 1f;
        }

        IEnumerator DelayBeforeStart() {
            yield return new WaitForSeconds(1f);
            backgroundBeforeStart.SetActive(false);
            UnPauseButton();
        }

        public void OpenPopupNewLevel(WeaponC weaponC, GameManager gameManager) {

            PauseButton();
            _gameManager = gameManager;

            iconWeapon.sprite = weaponC.weaponM.spriteForNewLevelPopup;
            nameWeapon.text = weaponC.weaponM.nameWeaponModel;
            if (weaponC.GetLevelWeapon() != 1) {
                levelWeapon.text ="Level: " + weaponC.GetLevelWeapon().ToString();

            }
            else {
                levelWeapon.text = "NEW";
            }
            describeWeapon.text = weaponC.weaponM.describeWeapon;
            uiPopupNewLevel.SetActive(true);
            _gettingWeaponC = weaponC;
            

        }

        public void NewLevelPopupButton1() {
            _gameManager.GetNewWeaponForPlayer(_gettingWeaponC);
            UnPauseButton();
            uiPopupNewLevel.SetActive(false);

        }
    }
}