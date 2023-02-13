using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraFollow: MonoBehaviour
    { 
        public float RotationAngleX;
        public float Distance;
        public float Offset;

        //private Transform _following;
        private Transform _gameObject;

        private void Start()
        {
            _gameObject = FindObjectOfType<Player>().gameObject.transform;
        }

        private void LateUpdate()
        {
            if(!_gameObject) return;

            Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
            
            Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();

            transform.position = position;
            transform.rotation = rotation;
        }

        public void ObjectToFollow(GameObject gameObject) =>
            _gameObject = gameObject.transform;

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _gameObject.position;
            followingPosition.y += Offset;
            return followingPosition;
        }
    }
}
