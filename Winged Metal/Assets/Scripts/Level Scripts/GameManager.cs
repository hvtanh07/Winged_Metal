using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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

    public void SetWinner(bool Damage)
    {
        if (Damage)
        {
            score.DamageScore++;
        }
        else
        {
            score.SpeedScore++;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
