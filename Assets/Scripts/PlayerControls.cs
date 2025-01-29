using Ubii.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{
    private CharacterController controller;
    private Camera camera;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material backupMaterial;
    private Color playerColor;

    [SerializeField] private float speed;
    [SerializeField] private float sprintMultiplier;

    [SerializeField] private Transform schieﬂpunkt;
    [SerializeField] private GameObject projektil;
    [SerializeField] private int projektilGeschwindigkeit;
    private LineRenderer lineRenderer;

    [SerializeField] private UbiiNode ubiiNode;

    private Vector3 moveInputValue;
    private Vector3 lookInputValue;
    private float moveSpeedValue;

    private bool canInteract;
    private int hitPoints;
    private IEnumerator coroutine;
    private float timer;
    private float colorDuration;
    private bool shouldChangeColor;

    [SerializeField] private float fireRate;
    private float fireRateTimeStamp;

    private void Start()
    {
        moveSpeedValue = speed;
        fireRateTimeStamp = 0;
        canInteract = true;
        hitPoints = 0;
        playerColor = backupMaterial.color;
        playerMaterial.color = backupMaterial.color;
        timer = 0;
        colorDuration = 1.2f;
        shouldChangeColor = false;
        lineRenderer = GetComponent<LineRenderer>();
        controller = GetComponent<CharacterController>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        StartTest();
    }

    private void OnMove(InputValue inputValue)
    {
        if (canInteract)
        {
            moveInputValue.x = inputValue.Get<Vector2>().x;
            moveInputValue.z = inputValue.Get<Vector2>().y;
        }
        else
        {
            moveInputValue = Vector3.zero;
        }
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
        if (canInteract)
        {
            if (Time.time > fireRateTimeStamp)
            {
                lineRenderer.SetPosition(0, schieﬂpunkt.position);

                float screenX = Screen.width / 2;
                float screenY = Screen.height / 2;

                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(new Vector3(screenX, screenY));

                if (Physics.Raycast(ray, out hit))
                {
                    lineRenderer.SetPosition(1, hit.point);
                    if (hit.collider.CompareTag("Target"))
                    {

                        //Debug.Log(collision.gameObject.tag + " getroffen!!!");
                        Destroy(hit.collider.gameObject);
                        GameObject.FindWithTag("Ziel").GetComponent<Portal>().incrementCount();

                    }
                    else if (hit.collider.CompareTag("Tower"))
                    {
                        hit.collider.gameObject.GetComponent<Tower>().decreaseHealth();
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, ray.origin + (camera.transform.forward * 50));
                }
                StartCoroutine(ShootLaser());


                /*
                //Debug.Log("sollte schieﬂen");
                //Aim();
                //GameObject tempProjektil = Instantiate(projektil, schieﬂpunkt.position, schieﬂpunkt.rotation);
                //tempProjektil.GetComponent<Rigidbody>().linearVelocity = schieﬂpunkt.forward * projektilGeschwindigkeit;
                */

                fireRateTimeStamp = Time.time + fireRate;
            }
        }
    }

    private IEnumerator ShootLaser()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
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

        if (shouldChangeColor)
        {
            playerMaterial.color = Color.Lerp(Color.red, playerColor, timer);
            if (timer < 1.2f)
            {
                timer += Time.deltaTime / colorDuration;
            }
        }

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

    /*
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
    */

    public void setInteract(bool interact)
    {
        canInteract = interact;
    }

    public void ouch()
    {
        setInteract(false);
        hitPoints++;
        playerMaterial.color = Color.red;
        timer = 0;
        shouldChangeColor = true;

        Debug.Log(hitPoints);

        coroutine = changeColor();
        StartCoroutine(coroutine);
    }

    private IEnumerator changeColor()
    {
        yield return new WaitForSeconds(1.2f);
        shouldChangeColor = false;
        setInteract(true);

        //playerMaterial.color = Color.Lerp(Color.red, playerColor, 2f);
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
