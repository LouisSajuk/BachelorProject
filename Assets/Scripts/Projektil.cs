using UnityEngine;

public class Projektil : MonoBehaviour
{
    bool canHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canHit = true;
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
            if (canHit)
            {
                canHit = false;
                //Debug.Log(collision.gameObject.tag + " getroffen!!!");
                Destroy(collision.gameObject);
                GameObject.FindWithTag("Ziel").GetComponent<Portal>().incrementCount();
                Destroy(gameObject);
            }
        }else if (collision.gameObject.CompareTag("Tower"))
        {
            collision.gameObject.GetComponent<Tower>().decreaseHealth();
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Objekt getroffen!");
            Destroy(gameObject);
        }
    }
}
