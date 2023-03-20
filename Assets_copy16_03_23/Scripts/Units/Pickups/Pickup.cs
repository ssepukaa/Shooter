using Assets.Scripts.Infrastructure.Managers;
using Assets.Scripts.Units.Pickups.Data;
using Assets.Scripts.Units.Players;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Units.Pickups {

    public class Pickup : Unit {
        public PickupModelData pickupData;
        private Transform _transform;
        private GameObject _mesh;

        private SoundManager _soundManager;
        private void Awake() {
            _transform =this.transform;
            _transform.position.Set(_transform.position.x, 0.5f, _transform.position.z);
            _mesh = pickupData.prefabMesh;
            _mesh = Instantiate(_mesh);
            _mesh.transform.SetParent(this.transform);
            _mesh.transform.position = this.transform.position;
            _mesh.transform.rotation = this.transform.rotation;


        }

        private void Start() {

            _soundManager = FindObjectOfType<SoundManager>();
        }

        private void Update() {
            _mesh.transform.Rotate(Vector3.up * 15f * Time.deltaTime, Space.World);
        }
        private void OnTriggerEnter(Collider collider) {
            IPlayer player = collider.GetComponent<IPlayer>();
            if (player!=null) {
                player.AddPickup(pickupData);
                _soundManager.PlaySound(pickupData.audioclipCollect, 1f, 1f);
                Destroy(gameObject);

            }
        }
    }
}
