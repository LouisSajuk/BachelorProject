using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int speed;
    private GameObject Player;
    private IEnumerator coroutine;
    private float playerSpeed;
    private float playerSprintMultiplier;


    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        playerSpeed = Player.GetComponent<PlayerControls>().getSpeed();
        playerSprintMultiplier = Player.GetComponent<PlayerControls>().getSprintMultiplier();
        coroutine = stopMovement();
    }

    void Update()
    {
        this.transform.Rotate(Vector3.up * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.GetComponent<PlayerControls>().setSpeed(0, 0);
            StartCoroutine(coroutine);

            Debug.Log(playerSpeed);
            Debug.Log(playerSprintMultiplier);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Boulder wurde getroffen!");
            if (!collision.gameObject.GetComponent<Boulder>().IsFlying())
            {
                //Debug.Log("Boulder wurde entfernt!");
                Destroy(collision.gameObject);
            }
        }
    }

    private IEnumerator stopMovement()
    {
        Debug.Log("Should not move for 2 Seconds");
        yield return new WaitForSeconds(2f);
        Player.GetComponent<PlayerControls>().setSpeed(playerSpeed, playerSprintMultiplier);

    }
}
