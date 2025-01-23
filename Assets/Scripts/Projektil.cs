using UnityEngine;

public class Projektil : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Objekt getroffen?");
        if (collision.gameObject.CompareTag("Target"))
        {

            GameObject.FindWithTag("Ziel").GetComponent<Portal>().incrementCount();

            Debug.Log(collision.gameObject.tag + " getroffen!!!");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Objekt getroffen!");
            Destroy(gameObject);
        }
    }
}
