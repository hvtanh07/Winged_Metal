using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float gameStartTime;
    public MatchScore score;
    public static GameManager gameManager;

    void Awake()
    {

        if (gameManager && gameManager != this)
        {
            Destroy(this);
            return;
        }
        gameManager = this;

    }
    void Start()
    {
        gameStartTime = Time.time;
    }


    public void SetWinner(bool TestingUnit)
    {
        float matchTime = Time.time - gameStartTime;
        score.currentAvgTime = ((score.currentAvgTime * score.Match + matchTime) / (score.Match + 1));
        score.Match += 1;
        
        if (TestingUnit)
        {
            score.Lose++;
        }
        else
        {
            score.Win++;
        }


        if (score.Lose == 0)
        {
            score.winRate = 1;
        }
        else
        {
            score.winRate = score.Win / score.Lose;
        }


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
