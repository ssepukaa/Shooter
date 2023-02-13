using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCEnemy : Unit, IEntity, IEnemy
    {
       public Transform firePoint;
        //public GameObject npcDeadPrefab;

        [HideInInspector] public Transform playerTransform;
        [HideInInspector] public EnemySpawner es;
        NavMeshAgent agent;
        float nextAttackTime = 0;
        [SerializeField] private GameObject explosionPrefab;
        [Header("BaseStats")]
        public float health = 100f;
        public float movementSpeed = 4f;
        public float rotationSpeed = 1f;
        public float jumpSpeed = 1.0F;
        public bool canMove = true;

        [Header("UniqClassStats")]
        public float attackDistance = 0.5f;
        public float damageValue = 5;
        public float attackRate = 0.5f;
        public Transform muzzle;

        public float GetHealthRef()
        {
            return health;
        }

        public float GetMovementSpeedRef()
        {
            return movementSpeed;
        }

        public float GetRotationSpeedRef()
        {
            return rotationSpeed;
        }

        public float GetJumpSpeedRef()
        {
            return jumpSpeed;
        }

        public bool GetCanMovRef()
        {
            return canMove;
        }

        // Start is called before the first frame update
        void Start()
        {
            
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = attackDistance;
            agent.speed = movementSpeed;
            es = FindObjectOfType<EnemySpawner>();
            playerTransform = es.player.transform;

            //Set Rigidbody to Kinematic to prevent hit register
            // if (GetComponent<Rigidbody>())
            // {
            //     GetComponent<Rigidbody>().isKinematic = true;
            // }
        }

        // Update is called once per frame
        void Update()
        {
            if (playerTransform != null)
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
                            if (hit.transform == playerTransform)
                            {
                                // Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.cyan);

                                IEntity player = hit.transform.GetComponent<IEntity>();
                                player.ApplyDamage(damageValue);
                            }
                        }
                    }


                }

                //Move towardst he player
                agent.destination = playerTransform.position;
                //Always look at player
                if (agent.destination != null)
                {
                    transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y,
                        playerTransform.position.z));

                }
            }
            else
            {

            }
            

        }



        public void Hit(Vector3 position, Quaternion rotation)
        {
            Instantiate(explosionPrefab, position, rotation);

        }
        public void ApplyDamage(float damageAmount)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                //Player is dead
                canMove = false;
                health = 0;
                Death();
            }
        }

        protected void Death()
        {
            MessageAfterDeath();
            Destroy(gameObject);

        }
        public virtual void MessageAfterDeath()
        {

        }
 
    }
}


