using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{

    public class FPSMovementController : MonoBehaviour
    {

        [Header("Main Settings")]
        public CharacterController TargetPlayer;
        public Camera TargetCamera;

        [Header("Animation Settings")]
        public bool usingAnimation;
        public Animator TargetAnimator;
        public KeyCode ReloadButton = KeyCode.R;

        [Header("Movement Settings")]
        public float PlayerSpeed;
        public float JumpSpeed;
        public float Gravity;
        Vector3 moveDirection;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (TargetPlayer.isGrounded)
            {

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = TargetCamera.transform.TransformDirection(moveDirection);
                moveDirection *= PlayerSpeed;

                if (Input.GetButton("Jump"))
                    moveDirection.y = JumpSpeed;

            }
            moveDirection.y -= Gravity * Time.deltaTime;
            TargetPlayer.Move(moveDirection * Time.deltaTime);

            if (usingAnimation)
            {
                if (moveDirection.x > 0 || moveDirection.z > 0)
                {
                    TargetAnimator.SetFloat("Speed", 2);
                } else
                {
                    TargetAnimator.SetFloat("Speed", -2);
                }
                if (Input.GetKeyDown(ReloadButton))
                {
                    TargetAnimator.SetTrigger("Reload");
                }
            }

        }
    }
}
