using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    [SerializeField] private int health;
    private Vector3 vector;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 10;
        vector = Vector3.up;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, Time.deltaTime * 5, 0f);


        if (transform.position.y > 10)
        {
            vector = Vector3.down;
        }
        else if (transform.position.y < 8)
        {
            vector = Vector3.up;
        }

        gameObject.transform.Translate(vector * (0.1f * Time.deltaTime));

        gameObject.transform.Translate(vector * (0.1f * Time.deltaTime));

        if (health == 0){
            Debug.Log("Should go back to StartingLevel");
            gameManager.nextSteuerung();
            SceneManager.LoadScene("StartingScene");
        }
    }

    public void decreaseHealth()
    {
        health--;
        Debug.Log(health);
    }
}
