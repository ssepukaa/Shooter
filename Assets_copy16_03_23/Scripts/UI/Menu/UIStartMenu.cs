using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Menu {
    public class UIStartMenu : MonoBehaviour {

        public void StartButtonClick() {
            SceneManager.LoadScene("BetweenScene");
        }

        public void ExitButtonClick() {
            Application.Quit();
        }
    }
}