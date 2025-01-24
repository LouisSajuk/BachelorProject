using Ubii.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera camera;

    [SerializeField] private float speed;
    [SerializeField] private float sprintMultiplier;

    [SerializeField] private Transform schieﬂpunkt;
    [SerializeField] private GameObject projektil;
    [SerializeField] private int projektilGeschwindigkeit;

    [SerializeField] private UbiiNode ubiiNode;

    private Vector3 moveInputValue;
    private Vector3 lookInputValue;
    private float moveSpeedValue;

    [SerializeField] private float fireRate;
    private float fireRateTimeStamp;

    private void Start()
    {
        moveSpeedValue = speed;
        fireRateTimeStamp = 0;
        StartTest();
    }

    private void OnMove(InputValue inputValue)
    {
        moveInputValue.x = inputValue.Get<Vector2>().x;
        moveInputValue.z = inputValue.Get<Vector2>().y;
    }

    private void OnLook(InputValue inputValue)
    {
        //moveInputValue.x = inputValue.Get<Vector2>().x;
        //moveInputValue.z = inputValue.Get<Vector2>().y;
    }

    private void OnSprint()
    {
        moveSpeedValue = speed * sprintMultiplier;
    }

    private void OnSprintOff()
    {
        moveSpeedValue = speed;
    }

    private void OnShoot()
    {
        if (Time.time > fireRateTimeStamp)
        {

            //Debug.Log("sollte schieﬂen");
            Aim();
            GameObject tempProjektil = Instantiate(projektil, schieﬂpunkt.position, schieﬂpunkt.rotation);
            tempProjektil.GetComponent<Rigidbody>().linearVelocity = schieﬂpunkt.forward * projektilGeschwindigkeit;

            fireRateTimeStamp = Time.time + fireRate;
        }
    }

    private void Aim()
    {
        float screenX = Screen.width / 2;
        float screenY = Screen.height / 2;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3(screenX, screenY));

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            schieﬂpunkt.LookAt(hit.point);
        }
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
            //float targetAngle = Mathf.Atan2(result.x, result.z) * Mathf.Rad2Deg;
            float targetAngle = Mathf.Atan2(result.x, result.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * (moveSpeedValue * Time.deltaTime));
        }

    }


    /*
    private void LookLogicMethod()
    {
        
        Vector3 result = moveInputValue.normalized;

        if (result.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(result.x, result.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }
    */


    private void Update()
    {
        MoveLogicMethod();
        //LookLogicMethod();
    }


    private async void StartTest()
    {
        if (ubiiNode == null)
        {
            Debug.Log("UbiiClient not found");
            return;
        }

        await ubiiNode.WaitForConnection();


        ServiceReply reply = await ubiiNode.CallService(new ServiceRequest
        {
            Topic = UbiiConstants.Instance.DEFAULT_TOPICS.SERVICES.DEVICE_GET_LIST,
            Device = new Ubii.Devices.Device

            {
                Name = "web-interface-smart-device",
                Tags = { new Google.Protobuf.Collections.RepeatedField<string>() { "claw" } },

            }
        });
        Debug.Log(reply);


        //reply.DeviceList.



        /*
        ServiceReply reply = await ubiiNode.CallService(new ServiceRequest
        {
            Topic = "/services/component/get_list",
            ComponentList = new Ubii.Devices.ComponentList
            {
                Elements = {
                    new Google.Protobuf.Collections.RepeatedField< Ubii.Devices.Component>(){
                        new Ubii.Devices.Component {
                            MessageFormat = "ubii.dataStructure.Vector3",
                            Tags = { new Google.Protobuf.Collections.RepeatedField<string>(){"claw"}}
                        }
                    }
                }
            }
        });
        Debug.Log(reply);
        */
    }

    public void setSpeed(float wert, float wert2)
    {
        moveSpeedValue = wert;
        sprintMultiplier = wert2;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getSprintMultiplier()
    {
        return sprintMultiplier;
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
