using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Tower : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI crystalHP;
    private Vector3 vector;
    //private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vector = Vector3.up;
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, Time.deltaTime * 5, 0f);


        if (transform.position.y > 6)
        {
            vector = Vector3.down;
        }
        else if (transform.position.y < 5)
        {
            vector = Vector3.up;
        }

        gameObject.transform.Translate(vector * (0.1f * Time.deltaTime));

        gameObject.transform.Translate(vector * (0.1f * Time.deltaTime));

        if (health == 0){
            Debug.Log("Should go back to StartingLevel");
            GameManager.Instance.nextSteuerung();
            SceneManager.LoadScene("StartingScene");
        }
    }

    public void decreaseHealth()
    {
        health--;
        Debug.Log(health);

        crystalHP.text = health.ToString() + " / 25 Crystal HP";
    }
}
