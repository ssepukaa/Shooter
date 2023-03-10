
using Assets.Scripts.Units.Players;


namespace Assets.Scripts.Weapons {
    public class WeaponC {
        public WeaponM weaponM;
        private Weapon _weapon;
        private Player _player;
        private int _ammo;
        private int _levelWeapon=1;

        public WeaponC(WeaponM weaponM, Weapon weapon, Player player) {
            this.weaponM = weaponM;
            _weapon = weapon;
            _player = player;
        }

        public int GetLevelWeapon() {
            return _levelWeapon;
        }
    }
}
