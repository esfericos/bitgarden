using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public void ChangeScene(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    
    public void ControlTime(float time)
    {
        Time.timeScale = time;
    }
}
