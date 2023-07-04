using UnityEngine;
using UnityEngine.EventSystems;


namespace Assets.Scripts.Player.Parkour.Thrid_Person_Controller
{
    public class JumpTest : MonoBehaviour
    {
        
        public float jumpForce = 5f; // Zıplama kuvveti
        public float gravity = 20f; // Yer çekimi katsayısı

        private CharacterController characterController;
        private Vector3 moveDirection;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (characterController.isGrounded)
            {
                // Yerdeyken zıplama tuşuna basıldığında
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;

            // Karakteri hareket ettirme
            characterController.Move(moveDirection * Time.deltaTime);
        }



        
    }

        
}