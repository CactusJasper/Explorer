using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
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

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> {}

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if(OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayer);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if(!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if(isGrounded || airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            Flip(targetVelocity.x);
        }
        // If the player should jump
        if(isGrounded && jump)
        {
            // Add a vertical force to the player.
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }


    private void Flip(float movement)
    {
        if(!Mathf.Approximately(0, movement))
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }

    // OLD Movement code
    /*
    public float moveSpeed = 10.0F;
    public float jumpForce = 10.0F;
    private Rigidbody2D _rigidbody;
    private InputHandler inputHandler;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        inputHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<InputHandler>();
    }

    private void Update()
    {
        float movement = GetMoveAxis() * Time.fixedDeltaTime * moveSpeed;
        transform.position += new Vector3(movement, 0, 0);
        // Handle Rotating the player when they change direction
        if (!Mathf.Approximately(0, movement))
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

        // Handle Player Jumping
        if(IsJumping() && Mathf.Abs(_rigidbody.velocity.y) < 0.001F)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private float GetMoveAxis()
    {
        float movement = 0.0F;
        if(Input.GetKey(inputHandler.moveLeft))
            movement += -1.0F;

        if(Input.GetKey(inputHandler.moveRight))
            movement += 1.0F;

        return movement;
    }

    private bool IsJumping()
    {
        return Input.GetKey(inputHandler.jump);
    }*/
}
