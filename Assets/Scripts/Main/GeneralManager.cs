using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void ChangeScene(string values)
    {
        
        string[] args = values.Split(',');
        
        Time.timeScale = float.Parse(args[1]);
        SceneManager.LoadScene(args[0]);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void TimeControl(float value)
    {
        Time.timeScale = value;
    }
}
