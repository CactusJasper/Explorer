using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [Header("Camera")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;
    [Tooltip("Current relative offset to the target")]
    public Vector3 offset;
    [Tooltip("Multiplier for the movement speed")]
    [Range(0f, 5F)]
    public float smoothSpeed = 0.125F;
    [Tooltip("Positions limits on the X-axis.")]
    public Vector2 moveLimitsX = new Vector2(-1.0F, 1.0F);
    [Tooltip("Positions limits on the Y-axis.")]
    public Vector2 moveLimitsY = new Vector2(-1.0F, 1.0F);

    private bool perspective = false;

    // Use this for initialization
    void Start()
    {
        if(!Camera.main.orthographic)
        {
            perspective = true;
        }
	}

    // LateUpdate is called every frame, if the Behaviour is enabled
    void LateUpdate()
    {
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, target.position + offset, smoothSpeed);
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, moveLimitsX.x, moveLimitsX.y);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, moveLimitsY.x, moveLimitsY.y);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, target.position.z + offset.z);

        if(perspective)
        {
            transform.LookAt(target);
        }
    }
}
