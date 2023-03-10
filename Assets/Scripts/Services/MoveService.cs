using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Services {
    public class MoveService : MyServices, IUpdateMoveService {
        [Header("Movement")] private CharacterController _characterController;
        private Vector3 moveDirectionLeft = Vector3.zero;
        private Vector3 moveDirectionRight = Vector3.zero;
        private Transform transform;
        private Player player;
        private float _deltairectAxisToFire;
        public bool canMove = true;
        private bool _checkSwitchRegisterIsWalking;
        private bool _checkSwitchRegisterIsAttacking;

        public MoveService(Transform transform) {
            this.transform = transform;
            _characterController = transform.gameObject.GetComponent<CharacterController>();
            player = _characterController.gameObject.GetComponent<Player>();
            _deltairectAxisToFire = Constants.deltaDirectJoystickForFire;
        }


        public void Move() {
            if (_characterController.isGrounded && canMove) {
                if (Input.GetButton("Jump")) {
                    moveDirectionLeft.y = player.GetJumpSpeed();
                }
            }


            moveDirectionLeft = new Vector3(HorizontalLeft(), 0, VerticalLeft());
            moveDirectionLeft -= Physics.gravity * Time.deltaTime;
            moveDirectionLeft.y = 0;
            _characterController.Move(moveDirectionLeft * Time.deltaTime * player.GetMoveSpeed());
            if (moveDirectionLeft != Vector3.zero) {
                player.AnimatorWalk();

            }




            if ((HorizontalRight() == 0 && VerticalRight() == 0) && (HorizontalLeft() != 0 || VerticalLeft() != 0)) {
                transform.forward = moveDirectionLeft;
                


            }

            if (HorizontalRight() > _deltairectAxisToFire ||
                HorizontalRight() < -_deltairectAxisToFire ||
                VerticalRight() > _deltairectAxisToFire ||
                VerticalRight() < -_deltairectAxisToFire) {
                player.FireWeapon();
                player.AnimatorAttack();




            }





            if (VerticalRight() != 0 || HorizontalRight() != 0) {
                Vector3 rotationVector = new Vector3(HorizontalRight(), 0, VerticalRight());
                transform.forward = rotationVector;
            }
        }

        public void UpdateMoveSpeed(float newSpeed) {
            player.SetMoveSpeed(newSpeed);
        }

        public float VerticalLeft() {
            return SimpleInput.GetAxis("Vertical");
        }

        public float HorizontalLeft() {
            return SimpleInput.GetAxis("Horizontal");
        }

        public float VerticalRight() {
            return SimpleInput.GetAxis("VerticalRight");
        }

        public float HorizontalRight() {
            return SimpleInput.GetAxis("HorizontalRight");
        }
    }
}