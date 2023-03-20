using System.Collections;
using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Units.Enemy.Data;
using Assets.Scripts.Units.Players;
using Assets.Scripts.Weapons;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Assets.Scripts.Units.Enemy {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class NPCEnemy : Unit, IEnemy, IEntity, IPlayerDead {
        [Header("Links")]
        // Добавить в инспекторе вручную
        public EnemyModelData modelData;

        public Transform muzzle;

        private Animator _animator;
        private Transform _playerTransform;
        private EnemySpawner _enemySpawner;
        private NavMeshAgent _agent;
        private bool _isPlayerDead = false;
        private GameManager _gameManager;

        // Сделать ссылки на Data Scriptableobject
        // [Header("VFX/SFX")]
        // private GameObject _onHitPrefab;
        // private AudioClip _deathAudioClip;
        // private GameObject _npcDeadPrefab;

        [Header("Stats")] [SerializeField] private float _health = 100f;
        [SerializeField] private float _exp = 2f;
        [Header("Movement")] private float _movementSpeed = 2f;
        private float _rotationSpeed = 1f;
        private float _jumpSpeed = 1.0F;
        private bool _canMove = true;

        [Header("Weapon")] [SerializeField] private float _attackDistance = 1f;
        [SerializeField] private float _damageValue = 5;
        private float _attackRate = 0.5f;
        private float _nextAttackTime = 0;
        private IEntity _entityPlayer;

        [Header("VFX/SFX")] private Vector3 _hitPosition;
        private Quaternion _hitRotation;


        private void Awake() {
            _gameManager = FindObjectOfType<GameManager>();
            InitialReference();
        }

        private void InitDataFromModel() {
            //Stats
            _health = modelData.health;
            //Movement
            _movementSpeed = modelData.movementSpeed;
            _rotationSpeed = modelData.rotationSpeed;
            _jumpSpeed = modelData.jumpSpeed;
            _canMove = modelData.canMove;
            //Weapon
            _attackDistance = modelData.attackDistance;
            _damageValue = modelData.damageValue;
            _exp = modelData.exp;
            _attackRate = modelData.attackRate;
            _nextAttackTime = modelData.nextAttackTime;
            _agent.stoppingDistance = _attackDistance;
            _agent.speed = _movementSpeed;
        }

        private void InitialReference() {
            _enemySpawner = FindObjectOfType<EnemySpawner>();
            _playerTransform = _enemySpawner._plTransform;

            
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            if(_playerTransform==null)return;
            _entityPlayer = _playerTransform.GetComponent<IEntity>();
        }


        // Start is called before the first frame update
        void Start() {
            InitDataFromModel();


            //Set Rigidbody to Kinematic to prevent hit register
            // if (GetComponent<Rigidbody>())
            // {
            //     GetComponent<Rigidbody>().isKinematic = true;
            // }
        }

        // Update is called once per frame
        void Update() {
            if (!_canMove || _isPlayerDead) return;
            if (_playerTransform != null && !_isPlayerDead) {
                if (_agent.remainingDistance - _attackDistance < 0.01f) {
                    if (Time.time > _nextAttackTime) {
                        _nextAttackTime = Time.time + _attackRate;

                        //Attack

                        StartCoroutine(DelayBeforeAttack());
                        RaycastHit hit;
                        

                        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, _attackDistance)) {
                            if (hit.transform == _playerTransform) {
                                //Debug.DrawLine(muzzle.position, muzzle.position + muzzle.forward * _attackDistance, Color.cyan);
                                _animator.SetTrigger("Attack");
                                IEntity player = hit.transform.GetComponent<IEntity>();
                                player.ApplyDamage(_damageValue);
                                player.Hit(hit.transform.position, Quaternion.identity);
                            }
                        }
                    }
                }

                //Move towardst he player
                _agent.destination = _playerTransform.position;
                _animator.SetTrigger("Walking");
                //Always look at player
                if (_agent.destination != null) {
                    transform.LookAt(new Vector3(_playerTransform.transform.position.x, transform.position.y,
                        _playerTransform.position.z));
                }
            }
            else { }
        }

        private IEnumerator DelayBeforeAttack() {
            yield return new WaitForSeconds(_attackRate);
        }


        public void Hit(Vector3 position, Quaternion rotation) {
            Instantiate(modelData.onHitPrefab, position, rotation);
            _enemySpawner.soundManager.PlaySound(modelData.GetSoundOfHit(),0.3f,1f);
            _hitPosition = position;
            _hitRotation = rotation;
        }

        public void ApplyDamage(float damageAmount) {
            _health -= damageAmount;

            if (_health <= 0) {
                //Player is dead
                _canMove = false;
                _health = 0;
                _hitPosition.y = 0.45f;
                Instantiate(modelData.onDeadPrefab, _hitPosition, _hitRotation);
               // _enemySpawner.soundManager.PlaySound(modelData.GetSoundOfDeath());
                _agent.ResetPath();

                Death();
            }
        }

        protected void Death() {
            _enemySpawner.soundManager.PlaySound(modelData.GetSoundOfDeath(),0.1f,Random.Range(0.8f,1.1f));
            MessageAfterDeath();
            gameObject.GetComponent<Collider>().enabled = false;
            _enemySpawner.OnEnemyDead(_exp);
            _animator.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }

        public void MessageAfterDeath() {
            _gameManager.CreatePickupOnEnemyDeath(this.transform);
        }
        public void OnPlayerDead() {
           _isPlayerDead = true ;
           
        }
    }
}