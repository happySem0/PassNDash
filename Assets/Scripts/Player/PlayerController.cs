using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Reference to the Move action from the Input System asset.")]
    public InputActionReference moveAction;
    [Tooltip("Movement speed in units per second.")]
    public float moveSpeed = 5f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private string GetDebuggerDisplay()
    {
        return $"Speed: {moveSpeed}, Velocity: {_rb?.linearVelocity}";
    }

    private void OnEnable()
    {
        if (moveAction != null)
            moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null)
            moveAction.action.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 input = Vector2.zero;
        if (moveAction != null)
            input = moveAction.action.ReadValue<Vector2>();

        // convert 2D input to 3D movement on the XZ plane
        Vector3 movement = new Vector3(input.x, 0f, input.y);
        _rb.linearVelocity = movement * moveSpeed;
    }
}
