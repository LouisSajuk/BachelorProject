using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject[] pages;
    private int counter = 0;




    public void PlayGame()
    {
        SceneManager.LoadScene("StartingScene");
    }


    private void OnContinue()
    {
        if (counter == 5)
            SceneManager.LoadScene("StartingScene");


        pages[counter].SetActive(true);
        if (counter != 0)
            pages[counter - 1].SetActive(false);
        counter++;

    }
}
