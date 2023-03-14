using System;
using Assets.Scripts.Units.Pickups.Data;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Units.Players {
    public interface IPlayer {
        public void AddPickup(PickupModelData data);
    }
}