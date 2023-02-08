﻿using Assets.Scripts.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCEnemy : MonoBehaviour, IEntity
    {
        public float attackDistance = 3f;
        public float movementSpeed = 4f;
        public float npcHP = 100;
        //How much damage will npc deal to the player
        public float npcDamage = 5;
        public float attackRate = 0.5f;
        public Transform firePoint;
        //public GameObject npcDeadPrefab;

        [HideInInspector]
        public Transform playerTransform;
        [HideInInspector]
        public EnemySpawner es;
        NavMeshAgent agent;
        float nextAttackTime = 0;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = attackDistance;
            agent.speed = movementSpeed;

            //Set Rigidbody to Kinematic to prevent hit register bug
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (agent.remainingDistance - attackDistance < 0.01f)
            {
                if (Time.time > nextAttackTime)
                {
                    nextAttackTime = Time.time + attackRate;

                    //Attack
                    RaycastHit hit;
                    if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackDistance))
                    {
                        if (hit.transform.CompareTag("Player"))
                        {
                           // Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.cyan);

                            IEntity player = hit.transform.GetComponent<IEntity>();
                            player.ApplyDamage(npcDamage);
                        }
                    }
                }
            }
            //Move towardst he player
            agent.destination = playerTransform.position;
            //Always look at player
            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
        }

        public void ApplyDamage(float points)
        {
            npcHP -= points;
            if (npcHP <= 0)
            {
                //Destroy the NPC
                //GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
                //Slightly bounce the npc dead prefab up
                //npcDead.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
                //Destroy(npcDead, 10);
               // es.EnemyEliminated(this);
                Destroy(gameObject);
            }
        }
    }
}

