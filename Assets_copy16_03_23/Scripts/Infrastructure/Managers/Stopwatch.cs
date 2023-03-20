using TMPro;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers {
    public class Stopwatch : MonoBehaviour {
        private float elapsedTime = 0.0f;
        private bool isRunning = false;
        [SerializeField] private GameObject _uiGameObject;
        private TextMeshProUGUI _timerText;

        void Start() {
            _timerText = _uiGameObject.GetComponentInChildren<TextMeshProUGUI>();
        }
        void Update() {
            if (isRunning) {
                elapsedTime += Time.deltaTime;
                UpdateTimerText();
            }
        }

        public void StartStopwatch() {
            isRunning = true;
        }

        public void StopStopwatch() {
            isRunning = false;
        }

        public void ResetStopwatch() {
            elapsedTime = 0.0f;
            isRunning = false;
            UpdateTimerText();
        }

        public float GetElapsedTime() {
            return elapsedTime;
        }

        private void UpdateTimerText() {
            int minutes = (int)(elapsedTime / 60.0f);
            int seconds = (int)(elapsedTime % 60.0f);
            int milliseconds = (int)((elapsedTime % 1.0f) * 100.0f);

            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            // _timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
            //Debug.Log(seconds);
        }
    }
}
// To use this code, you can create a new GameObject in your scene and attach the Stopwatch script
   // to it.Then, you can call the StartStopwatch() method to start the stopwatch,
   // StopStopwatch() to stop it, ResetStopwatch() to reset the elapsed time to zero
   // and stop the stopwatch, and GetElapsedTime() to get the current elapsed time in seconds.




      