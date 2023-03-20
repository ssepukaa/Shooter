using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.WeaponUI {
    public class BulletsInMagazineProgressBar: MonoBehaviour {
    [SerializeField] private Image imgFiller;
    [SerializeField] private TextMeshProUGUI textValue;

    public void SetValue(int bulletsInMagazine, int bulletsInInventory, int bulletsPerMagazine) {
        this.imgFiller.fillAmount = (float) bulletsInMagazine/ bulletsPerMagazine;
        this.textValue.text = $"{bulletsInMagazine} / {bulletsInInventory}";
    }

    }
}
