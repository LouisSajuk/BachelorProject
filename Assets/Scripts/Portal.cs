using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Scene scene;
    private int targetCount;
    private void Start()
    {
        targetCount = 0;
        scene = SceneManager.GetActiveScene();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (scene.name == "StartingScene")
            {
                SceneManager.LoadScene("Level_1");
            }
            else if (scene.name == "Level_1")
            {
                if(targetCount == 9)
                SceneManager.LoadScene("Level_2");
            }
            else if (scene.name == "Level_2")
            {
                SceneManager.LoadScene("Level_3");
            }
            else if (scene.name == "Level_3")
            {
                SceneManager.LoadScene("StartingScene");
            }
        }
    }

    public void incrementCount()
    {
        targetCount++;
        Debug.Log("targetCount = " + targetCount);
    }
}
