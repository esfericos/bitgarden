using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool start; // indica quando os inimigos comecaram a nascer
    public bool GameOver;

    public void Start()
    {
        // imobilizar os inimigos (n sei como esta sendo feito)
    }

    public void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0) start = true;
        if (start)
        {
            if (Core.IsDead)
            {
                // game over
                ChangeScene("GameOver,1");
            }
            else if (enemies.Length == 0)
            {
                // you win
                newWave();
            }
        }
    }

    public void newWave()
    {
        // setar um novo core
        var graphHandler = GameObject.FindGameObjectWithTag("GraphData").GetComponent<GraphManager>();
        graphHandler.NewWave();
    }


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
