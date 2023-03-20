
using UnityEngine;

namespace Assets.Scripts.UI.WeaponUI {
    public class UIWidgetBulletsBar: MonoBehaviour, IBulletChangedUI {
        [SerializeField] private BulletsInMagazineProgressBar _bulletsProgressBar;

        
        public void OnPlayerBulletsValueChanged(int bulletsInMagazine, int bulletsInInventory, int bulletsPerMagazine) {
            this._bulletsProgressBar.SetValue(bulletsInMagazine, bulletsInInventory, bulletsPerMagazine);

        }

        
    }
}
