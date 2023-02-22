using System;
using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class UIWidgetLifeBar: MonoBehaviour {
        [SerializeField] private ProgressBar _progressBar;

        private void OnEnable() {
            var player = FindObjectOfType<Player>();
            player.OnPlayerHealthValueChangedEvent += OnPlayerHealthValueChanged;
            this._progressBar.SetValue(player.HealthPercent());

        }

        private void OnPlayerHealthValueChanged(float newValuePercent) {
            this._progressBar.SetValue(newValuePercent);
            
        }

        private void OnDisable() {
            var player = FindObjectOfType<Player>();
            if (player) {
                player.OnPlayerHealthValueChangedEvent -= OnPlayerHealthValueChanged;
            }
        }
    }
}
