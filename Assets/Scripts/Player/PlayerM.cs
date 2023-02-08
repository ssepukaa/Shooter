using System.Collections.Generic;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Player
{
   [CreateAssetMenu(fileName = "Data", menuName = "Player/PlayerModel", order = 1)]
      
   public class PlayerM: ScriptableObject
    {
        [Header("Stats")]
        public int health = 500;
        public float moveSpeed = 7f;
        public float rotationSpeed = 7f;
        public float jumpSpeed = 8.0F;
        public List<WeaponC> weaponList; 

        //public float deltaDirectJoystickForFire = 0.9f;
        //[SerializeField] private float gravity = 20.0F;
    }
}
