using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the first level; replace "Level1" with the name of your first level scene
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
