using Ubii.Services;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using Unity.Cinemachine;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    private CharacterController controller;
    private Camera camera;

    private CinemachineInputAxisController baseAxisController;
    private TiltInputController tiltInputController;
    private bool axisInteractable = true;

    private PlayerInput input;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material backupMaterial;
    private Color playerColor;

    [SerializeField] private float speed;
    [SerializeField] private float sprintMultiplier;

    [SerializeField] private Transform schieﬂpunkt;
    [SerializeField] private GameObject projektil;
    [SerializeField] private int projektilGeschwindigkeit;
    private LineRenderer lineRenderer;

    private Vector3 moveInputValue;
    private Vector3 lookInputValue;
    private float moveSpeedValue;

    private bool canInteract;
    private int hitPoints;
    private IEnumerator coroutine;
    private float timer;
    private float colorDuration;
    private bool shouldChangeColor;

    [SerializeField] private AudioClip ouchSound;
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private float fireRate;
    private float fireRateTimeStamp;
    private int counter = 0;

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
        input = GetComponent<PlayerInput>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        CinemachineInputAxisController[] axisController = GameObject.Find("Third Person Camera").GetComponents<CinemachineInputAxisController>();
        baseAxisController = axisController[0];
        tiltInputController = GameObject.Find("Third Person Camera").GetComponent<TiltInputController>();

        axisInteractable = false;
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

    private void OnSprint()
    {
        moveSpeedValue = speed * sprintMultiplier;

        if(GameManager.Instance.Steuerung == 4)
        {
            baseAxisController.enabled = false;
            tiltInputController.enabled = true;
        }
    }

    private void OnSprintOff()
    {
        moveSpeedValue = speed;

        if (GameManager.Instance.Steuerung == 4)
        {
            baseAxisController.enabled = true;
            tiltInputController.enabled = false;
        }
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
                        counter++;
                        Destroy(hit.collider.gameObject);
                        GameObject.Find("targets").GetComponent<TextMeshProUGUI>().text = counter.ToString() + " / 9";
                        GameObject.FindWithTag("Ziel").GetComponent<Portal>().incrementCount();

                    }
                    else if (hit.collider.CompareTag("Tower"))
                    {
                        hit.collider.gameObject.GetComponent<Tower>().decreaseHealth();
                    }else if (hit.collider.CompareTag("Schild"))
                    {
                        GameManager.Instance.switchSteuerung(hit.collider.transform.parent.GetChild(1).GetComponent<Portal>().getSteuerung());
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, ray.origin + (camera.transform.forward * 50));
                }
                StartCoroutine(ShootLaser());

                SoundManager.instance.PlaySoundClip(laserSound, schieﬂpunkt, 1f);

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

    private void Update()
    {
        MoveLogicMethod();

        if (shouldChangeColor)
        {
            playerMaterial.color = Color.Lerp(Color.red, playerColor, timer);
            if (timer < 1.2f)
            {
                timer += Time.deltaTime / colorDuration;
            }
        }

        if(transform.position.y != 0)
        {
            Vector3 newPos = new Vector3(transform.position.x, 1, transform.position.z);
            transform.position = newPos;
        }
    }

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

        SoundManager.instance.PlaySoundClip(ouchSound, schieﬂpunkt, 1f);

        coroutine = changeColor();
        StartCoroutine(coroutine);
    }

    private IEnumerator changeColor()
    {
        yield return new WaitForSeconds(1.2f);
        shouldChangeColor = false;
        setInteract(true);
    }
}
