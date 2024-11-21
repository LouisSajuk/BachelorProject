using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;

    private Vector3 moveInputValue;

    private void OnMove(InputValue inputValue)
    {
        moveInputValue.x = inputValue.Get<Vector2>().x;
        moveInputValue.z = inputValue.Get<Vector2>().y;
    }

    private void OnJump(InputValue inputValue)
    {
        if (isGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void MoveLogicMethod()
    {
        rb.AddForce(moveInputValue * speed);

    }

    private void FixedUpdate()
    {
        MoveLogicMethod();

        if (!isGrounded())
            pushDown();
    }

    private void pushDown()
    {
        Debug.Log("pushing down");
        //rb.linearVelocity += Vector3.down * (Time.fixedDeltaTime * fallMultiplier);
        rb.AddForce(Vector3.down * fallMultiplier, ForceMode.Impulse);
    }

    private bool isGrounded()
    {
        return rb.linearVelocity.y == 0;
    }
}
