
using System;

namespace Assets.Scripts.UI.WeaponUI {
    [Flags]
    public enum StateWeaponItemUI {
        None = 1 <<0,
        Idle = 1 <<1,
        Reload = 1 <<2,
        Holder = 1 <<3,
    }
}
