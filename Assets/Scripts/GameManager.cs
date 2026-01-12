using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject virtualCamera;
    public GameObject resultPopup;

    public float timeLimit = 30;
    public int lives = 3; 

    public TextMeshProUGUI scoreLabel;

    public Player player;
    public Lives lifeDisplayer;

    public bool isCleared;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifeDisplayer.SetLives(lives);
        isCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;
        //scoreLabel.text = "TimeLimit :" + timeLimit.ToString("#");
        scoreLabel.text = "TimeLeft :" + ((int)timeLimit).ToString();

        if(timeLimit < 0)
        {
            GameOver();
        }
    }


    public void AddTime(float time)
    {
        timeLimit += time;
    }

    public void Die()
    {
        virtualCamera.SetActive(false);
        
        lives--;
        lifeDisplayer.SetLives(lives);
        //Invoke("Restart", 2);
        Invoke(nameof(Restart), 1.0f);
    }

    public void Restart()
    {
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            virtualCamera.SetActive(true);
            player.Restart();
        }
    }

    public void StageClear()
    {
        isCleared = true;
        resultPopup.SetActive(true);
    }

    public void GameOver()
    {
        isCleared = false;
        resultPopup.SetActive(true);
    }
}
