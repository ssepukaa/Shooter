namespace Assets.Scripts.UI.Weapon {
    public interface IBulletChangedUI {
        public void OnPlayerBulletsValueChanged(int bulletsInMagazine, 
            int bulletsInInventory,
            int bulletsPerMagazine);
    }
}
