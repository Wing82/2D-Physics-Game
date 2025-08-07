using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //groundCheck Variables
    [SerializeField, Range(0.01f, 0.1f)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;
    private Transform groundCheck;

    void Start()
    {
        // groundCheck Initalization
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform.GetChild(1));
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }

    public bool isGrounded()
    {
        if (!groundCheck) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}
