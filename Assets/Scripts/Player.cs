using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
       
        [Header("Stats")] 
        [SerializeField] private int health = 500;

        private MoveService moveService;

        private void Start()
        {
            moveService = new MoveService(this.transform);
        }

        private void Update()
        {
            moveService.Move();
        }

        
    }
}