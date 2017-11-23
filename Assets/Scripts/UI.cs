using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    public GameObject Screen_Menu;
    public GameObject Screen_End;

    public GameObject Button_Start;
    public GameObject Button_Resume;
    public GameObject Button_Pause;

    public GameObject Text_Score;
    public GameObject Text_Highscore;

    public bool GameStarted;
    public bool GameRunning;
    public bool GameOver;

    int PrefScore;

    void Start()
    {
        GameStarted = false;
        GameRunning = false;
        GameOver    = false;

        Screen_End.SetActive(false); 

        Time.timeScale = 0;
    }

    void Update()
    {
        if (GameRunning)
        {
            Screen_Menu.SetActive(false);

            Button_Pause.SetActive(true);
            Text_Score.SetActive(true);
            Text_Highscore.SetActive(true);

            SaveScore();

            int highScore = PlayerPrefs.GetInt("HighScore");
            Text_Highscore.GetComponent<UnityEngine.UI.Text>().text = "HighScore: " + highScore;

            Time.timeScale = 1;
        }
        else
        {
            Screen_Menu.SetActive(true);

            Button_Pause.SetActive(false);
            Text_Score.SetActive(false);
            Text_Highscore.SetActive(false);

            Time.timeScale = 0;
            if (GameStarted == true)
            {
                Button_Start.SetActive(false);
                Button_Resume.SetActive(true);
            }
            else
            {
                Button_Start.SetActive(true);
                Button_Resume.SetActive(false);
            }
        }
        if (GameOver)
        {
            Button_Pause.SetActive(false);
            Screen_End.SetActive(true);
            Time.timeScale = 0;

            //SaveScore();

            int highScore = PlayerPrefs.GetInt("HighScore");
            Text_Highscore.GetComponent<UnityEngine.UI.Text>().text = "HighScore: " + highScore;

        }
    }

    public void Play()
    {
        GameRunning = true;
        GameStarted = true;
    }

    public void Pause()
    {
        GameRunning = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void SaveScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int oldScore = PlayerPrefs.GetInt("HighScore");

            if (oldScore < Text_Score.GetComponent<Scoring>().PlayerScore)
                PlayerPrefs.SetInt("HighScore", Text_Score.GetComponent<Scoring>().PlayerScore);
        }
        else
            PlayerPrefs.SetInt("HighScore", Text_Score.GetComponent<Scoring>().PlayerScore);
    }
}