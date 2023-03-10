using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure {
    public class BetweenSceneScriptOnInterface : MonoBehaviour {

        private void Awake() {
            StartCoroutine(DelayBeforeStart());
            
        }

       
       

        IEnumerator DelayBeforeStart() {
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene("GameScene");

        }
    }
}