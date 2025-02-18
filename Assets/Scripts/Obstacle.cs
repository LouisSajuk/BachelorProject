using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int speed;
    //private GameObject Player;
    private PlayerControls PlayerControls;
    //private Rigidbody rg;
    private IEnumerator coroutine;
    private float playerSpeed;
    private float playerSprintMultiplier;


    private void Start()
    {
        PlayerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        //playerSpeed = PlayerControls.getSpeed();
        //playerSprintMultiplier = PlayerControls.getSprintMultiplier();

        //coroutine = stopMovement();
    }

    void Update()
    {
        this.transform.Rotate(Vector3.up * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PerformanceCatcher.Instance.incOuchFire();
            Debug.Log("Ouch by Obstacle!!!");
            PlayerControls.ouch();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!collision.gameObject.GetComponent<Boulder>().IsFlying())
            {
                Destroy(collision.gameObject);
            }
        }
    }

    /*
    private IEnumerator stopMovement()
    {
        Debug.Log("Should not move for 2 Seconds");
        yield return new WaitForSeconds(1.2f);

        //rg.constraints = RigidbodyConstraints.None;
        //PlayerControls.setMovable(true);   
        //PlayerControls.setSpeed(playerSpeed, playerSprintMultiplier);
    }
    */
}
