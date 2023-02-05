using UnityEngine;

namespace Assets.Scripts
{
    public class MoveService : IMoveable
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float jumpSpeed = 8.0F;
        [SerializeField] private float gravity = 20.0F;
        private CharacterController _characterController;
        private Vector3 moveDirectionLeft = Vector3.zero;
        private Vector3 moveDirectionRight = Vector3.zero;
        private Transform transform;

        public MoveService(Transform transform)
        {
            this.transform = transform;
            _characterController = transform.gameObject.GetComponent<CharacterController>();
        }


        public void Move()
        {
            if (_characterController.isGrounded)
            {

                if (Input.GetButton("Jump"))
                    moveDirectionLeft.y = jumpSpeed;
            }



            moveDirectionLeft = new Vector3(HorizontalLeft(), 0, VerticalLeft());
            moveDirectionLeft -= Physics.gravity * Time.deltaTime;
            moveDirectionLeft.y = 0;
            _characterController.Move(moveDirectionLeft * Time.deltaTime * moveSpeed);
            //transform.forward = moveDirectionLeft;
            //Vector3 lookDirection = new Vector3(Input.GetAxisRaw("RightHoriz"), 0, Input.GetAxisRaw("RightVert"));
            //transform.rotation = Quaternion.LookRotation(lookDirection);
            if (VerticalRight()!=0 || HorizontalRight()!=0)
            {
                Vector3 rotationVector = new Vector3(HorizontalRight(), 0, VerticalRight());
                transform.forward = rotationVector;
            }
            


        }

        public void UpdateMoveSpeed(float newSpeed)
        {
            moveSpeed = newSpeed;
        }

        public float VerticalLeft()
        {
            return SimpleInput.GetAxis("Vertical");
        }

        public float HorizontalLeft()
        {
            return SimpleInput.GetAxis("Horizontal");
        }
        public float VerticalRight()
        {
            return SimpleInput.GetAxis("VerticalRight");
        }

        public float HorizontalRight()
        {
            return SimpleInput.GetAxis("HorizontalRight");
        }
    }
}