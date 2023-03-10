namespace Assets.Scripts.UI.WeaponUI {
    public interface IBulletChangedUI {
        public void OnPlayerBulletsValueChanged(int bulletsInMagazine, 
            int bulletsInInventory,
            int bulletsPerMagazine);
    }
}
