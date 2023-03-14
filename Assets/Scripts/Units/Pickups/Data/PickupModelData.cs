using Unity.VisualScripting;
using UnityEngine;


namespace Assets.Scripts.Units.Pickups.Data {
    public enum TypeGroupOfPickups {
        none = 0,
        ammo = 10,
        skill = 20,

    }
    public enum TypePickup {
        none = 0,
        bulletsRifle = 10,
        bulletsShotgun = 20,
        bulletsSniperRifle = 30,
        bulletsMinigun= 40,
        bulletsRPG = 50,
        bulletsFlamethrower = 60,
        health = 70,

    }
    [CreateAssetMenu(fileName = "Data", menuName = "Unit/Pickup")]
    public class PickupModelData: ScriptableObject {
        public string nameModel;
        public int valuePickup = 10;
        public GameObject prefabBullet;
        public GameObject prefabMesh;
        public TypePickup typePickup;
        public AudioClip audioclipCollect;
        public TypeGroupOfPickups typeGroupOfPickups;
    }

}

