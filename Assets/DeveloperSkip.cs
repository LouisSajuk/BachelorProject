using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperSkip : MonoBehaviour
{
    private Scene scene;
    private GameManager gameManager;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (scene.name == "Level_1")
            {
                SceneManager.LoadScene("Level_2");
            }
            else if (scene.name == "Level_2")
            {
                SceneManager.LoadScene("Level_3");
            }
            else if (scene.name == "Level_3")
            {
                Debug.Log("Should go back to StartingLevel");
                Debug.Log("Current Steuerung : " + gameManager.getSteuerung());
                gameManager.nextSteuerung();
                SceneManager.LoadScene("StartingScene");
            }
        }
    }
}
