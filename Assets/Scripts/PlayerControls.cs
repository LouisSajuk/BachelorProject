using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform camera;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject cameraCylinder;


    //[SerializeField] private float jumpForce;
    //[SerializeField] private float fallMultiplier;

    private Vector3 moveInputValue;
    private Vector3 lookInputValue;
    private float moveSpeedValue;

    private void Start()
    {
        moveSpeedValue = speed;

    }

    private void OnMove(InputValue inputValue)
    {
        moveInputValue.x = inputValue.Get<Vector2>().x;
        moveInputValue.z = inputValue.Get<Vector2>().y;
    }

    private void OnLook(InputValue inputValue)
    {
        /*
        if (inputValue.Get<Vector2>().sqrMagnitude < 0.01)
        {
            lookInputValue = Vector3.zero;
        }
        else
        {
            //Debug.Log("moving cylinder");
            lookInputValue.y = -inputValue.Get<Vector2>().x;
            lookInputValue.x = inputValue.Get<Vector2>().y;
        }
        */
    }

    private void OnSprint()
    {
        moveSpeedValue = speed * sprintMultiplier;
    }

    private void OnSprintOff()
    {
        moveSpeedValue = speed;
    }

    /*
    private void OnJump(InputValue inputValue)
    {
        if (isGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    */

    private void MoveLogicMethod()
    {
        Vector3 result = moveInputValue.normalized;

        if (result.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(result.x, result.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * (moveSpeedValue * Time.deltaTime));
            //rb.linearVelocity = result;
        }

        //rb.AddForce(moveInputValue * speed);
    }

    private void LookLogicMethod()
    {
        Vector3 result = lookInputValue * (rotateSpeed * Time.fixedDeltaTime);
        Debug.Log(result);

        /*

        if (rotate.sqrMagnitude < 0.01)
            return;


        m_Rotation.y += rotate.x * (rotateSpeed * Time.deltaTime);
        m_Rotation.x = Mathf.Clamp(m_Rotation.x - rotate.y * scaledRotateSpeed, -89, 89);
        transform.localEulerAngles = m_Rotation;
        */

        cameraCylinder.GetComponent<Rigidbody>().angularVelocity = result;

        //cameraCylinder.transform.localEulerAngles = result;

        /*
        Vector3 result = moveInputValue * (moveSpeedValue * Time.fixedDeltaTime);
        rb.linearVelocity = result;
        */

        //rb.AddForce(moveInputValue * speed);
    }

    private void Update()
    {
        MoveLogicMethod();
        //LookLogicMethod();

        /*
        if (!isGrounded())
            pushDown();
        */
    }

    /*

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
    */
}
