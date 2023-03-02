using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.UI.Exp {
    public class ExpProgressBar: MonoBehaviour {
        [SerializeField] private Image imgFiller;
        [SerializeField] private TextMeshProUGUI textValues;
        [SerializeField] private TextMeshProUGUI textValuesLevel;

        public void SetValue(float valueExp, float valueMaxExp, int valueFrag, int level) {
            this.imgFiller.fillAmount =valueExp / valueMaxExp;
            this.textValues.text = $"{valueFrag}  dead zombies";
            this.textValuesLevel.text = $"{level}";
        }
    }
}
