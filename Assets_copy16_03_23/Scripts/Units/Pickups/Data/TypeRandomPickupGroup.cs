
using UnityEngine;

namespace Assets.Scripts.Units.Pickups.Data {

    [CreateAssetMenu(fileName = "Data", menuName = "Unit/Service/RandomPickupGroup")]
    public class TypeRandomPickupGroup:ScriptableObject {
        public string name;
        public RandomPickupModelData[] randomPickupModelsData;
    }
}
