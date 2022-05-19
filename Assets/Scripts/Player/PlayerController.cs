using UnityEngine;
using UnityEngine.Events;

namespace Explorer.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float jumpForce = 7.24F;                          // Amount of force added when the player jumps.
        [Range(0, 0.3F)] [SerializeField] private float movementSmoothing = 0.05F;  // How much to smooth out the movement
        [SerializeField] private bool airControl = false;                         // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask groundLayer;                          // A mask determining what is ground to the character
        [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.

        const float groundedRadius = 0.2F; // Radius of the overlap circle to determine if grounded
        private bool isGrounded;            // Whether or not the player is grounded.
        private Rigidbody2D rb;
        private Vector3 velocity = Vector3.zero;

        [Header("Events")]
        [Space]
        public UnityEvent onLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> {}

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();

            if(onLandEvent == null)
                onLandEvent = new UnityEvent();
        }

        private void FixedUpdate() {
            bool wasGrounded = isGrounded;
            isGrounded = false;

            // The player is grounded if a circle-cast to the ground check position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            // ReSharper disable once Unity.PreferNonAllocApi
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayer);
            foreach(Collider2D c in colliders) {
                if(c.gameObject == gameObject) continue;
                isGrounded = true;
                if(!wasGrounded)
                    onLandEvent.Invoke();
            }
        }


        public void Move(float move, bool jump) {
            //only control the player if grounded or airControl is turned on
            if(isGrounded || airControl) {
                // Move the character by finding the target velocity
                Vector2 velocity1 = rb.velocity;
                Vector3 targetVelocity = new Vector2(move * 10f, velocity1.y);
                // And then smoothing it out and applying it to the character
                rb.velocity = Vector3.SmoothDamp(velocity1, targetVelocity, ref velocity, movementSmoothing);

                Flip(targetVelocity.x);
            }
            // If the player should jump
            if(!isGrounded || !jump) return;
            // Add a vertical force to the player.
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }


        private void Flip(float movement) {
            if(!Mathf.Approximately(0, movement))
                transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
    }
}
