using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //groundCheck Variables
    [Header("Ground Check Setting")]
    [SerializeField] private Transform groundCheck;
    [SerializeField, Range(0.01f, 0.1f)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;

    // Check if the groundCheck is touching the ground layer
    public bool isGrounded()
    {
        if (!groundCheck) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
