using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units.Pickups.Data {

    [CreateAssetMenu(fileName = "Data", menuName = "Unit/Service/RandomPickup")]

    public class RandomPickupModelData : ScriptableObject {
        public string name;
        public PickupModelData[] pickupModels;
        public PickupModelData GetRandomModelData() {

            return pickupModels[Random.Range(0, pickupModels.Length)];
        }
    }
}