using Explorer.Input;
using UnityEngine;

namespace Explorer.Player {
    [RequireComponent(typeof(PlayerController))]
    public class PlayerMovement : MonoBehaviour
    {
        public float playerSpeed = 3.18F;

        private PlayerController playerController;
        private InputHandler inputHandler;

        private float xMove = 0.0F;

        private void Awake() {
            playerController = GetComponent<PlayerController>();
            inputHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<InputHandler>();
        }

        private void Update() {
            xMove = GetMoveAxis() * (playerSpeed * 10.0F);
        }

        private void FixedUpdate() {
            playerController.Move(xMove * Time.fixedDeltaTime, IsJumping());
        }

        private float GetMoveAxis() {
            float movement = 0.0F;
            if(UnityEngine.Input.GetKey(inputHandler.moveLeft))
                movement += -1.0F;

            if(UnityEngine.Input.GetKey(inputHandler.moveRight))
                movement += 1.0F;

            return movement;
        }

        private bool IsJumping() {
            return UnityEngine.Input.GetKey(inputHandler.jump);
        }
    }
}
