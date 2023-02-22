
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI {
    public class ProgressBar: MonoBehaviour {
        [SerializeField] private Image imgFiller;
        [SerializeField] private TextMeshProUGUI textValue;

        public void SetValue(float valueNormalized) {
            this.imgFiller.fillAmount = valueNormalized;
            var valueInPercent = Mathf.RoundToInt(valueNormalized * 100f);
            this.textValue.text = $"{valueInPercent}%";
        }

    }
}
