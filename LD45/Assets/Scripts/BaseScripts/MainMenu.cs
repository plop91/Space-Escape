using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject levelSelectMenu;
    private bool optionsEnabled = false;
    private bool levelSelectEnabled = false;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ToggleLevelSelect()
    {
        levelSelectEnabled = !levelSelectEnabled;
        if (levelSelectEnabled)
        {
            levelSelectMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        else if (!levelSelectEnabled)
        {
            mainMenu.SetActive(true);
            levelSelectMenu.SetActive(false);
        }
    }
    public void ToggleOptionsMenu()
    {
        optionsEnabled = !optionsEnabled;
        if (optionsEnabled)
        {
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        else if (!optionsEnabled)
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoToLevel(int i)
    {
        SceneManager.LoadScene(i);
    }
}
