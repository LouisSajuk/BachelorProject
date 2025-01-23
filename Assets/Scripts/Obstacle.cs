using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int speed;


    void Update()
    {
        this.transform.Rotate(Vector3.up * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player wurde getroffen!");
        }
    }
}
