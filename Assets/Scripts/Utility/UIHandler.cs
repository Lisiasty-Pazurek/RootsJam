using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;

    public GameObject activeObjects;

    public int Score = 0;
    public int enemyScore = 0;
    [SerializeField] public int maxScore = 6;
    [SerializeField] public Text hpText;
    [SerializeField] public Text Points;

    public List<Canvas> uiCanvas; 
    [SerializeField] public Canvas startGame;
    [SerializeField] public Canvas playGame;
    [SerializeField] public Canvas endGame;
    [SerializeField] public Canvas deadGame;   
    [SerializeField] public Image cantRun;

    public int hearts = 5;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        ChangeHP();
        CheckPoints();
        ChangeScore();
        cantRun.enabled = playerController.runTime <= 0;
    }

    public void StartGame()
    {
        startGame.enabled = false;
        playGame.enabled = true;
        activeObjects.SetActive(true);
    }

    void ChangeHP()
    {
        hearts = playerController.cHP;
        hpText.text = hearts.ToString();
    }


    void ChangeScore()
    {
        Points.text = Score.ToString();
    }

    void CheckPoints()
    {
        if (Score >= maxScore) 
        {
            PlayerWin();
        }

        if (playerController.cHP <1)
        {PlayerDied();}
    }

    void PlayerDied ()
    {
        deadGame.enabled = true;
        activeObjects.SetActive(false);
    }

    void PlayerWin ()
    {
        endGame.enabled = true;
        activeObjects.SetActive(false);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Level001");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
