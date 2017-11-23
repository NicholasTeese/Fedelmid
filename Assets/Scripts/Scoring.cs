using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int PlayerScore;
    public GameObject Player;
    UnityEngine.UI.Text Score;
    UnityEngine.UI.Text Highscore;

    void Start()
    {
        // Initialize Score
        PlayerScore = 0;
        // Apply score to text window
        Score = GetComponent<UnityEngine.UI.Text>();
        Highscore = GetComponent<UnityEngine.UI.Text>();
    }
	
	void Update()
    {
        // Set score value to equal Players x position
        if((int)Player.transform.position.x > PlayerScore)
            PlayerScore = (int)Player.transform.position.x;

        // Update text
        Score.text = "Score: " + PlayerScore;
    }
}