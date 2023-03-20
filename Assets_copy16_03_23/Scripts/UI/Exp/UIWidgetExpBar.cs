using UnityEngine;

namespace Assets.Scripts.UI.Exp {
    public class UIWidgetExpBar: MonoBehaviour, IExpUI {
        [SerializeField] private ExpProgressBar _expProgressBar;

        public void OnPlayerExpValueChanged(float valueExp, float valueMaxExp, int valueFrag, int level) {
           this._expProgressBar.SetValue(valueExp,  valueMaxExp, valueFrag, level);
        }
    }
}
