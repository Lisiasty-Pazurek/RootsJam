using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject player;

    public int Score = 0;
    [SerializeField] public int maxScore = 6;
    [SerializeField] public Text hpText;
    [SerializeField] public Text Points;

    [SerializeField] public Canvas EndGame;
    [SerializeField] public Canvas DeadGame;   

    public int hearts = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        ChangeHP();
        ChangeAcorns();
        CheckPoints();
    }
    // Update is called once per frame
    void ChangeHP()
    {
        hearts = player.GetComponent<PlayerController>().cHP;
        hpText.text = hearts.ToString();
    }


    void ChangeAcorns()
    {
        Points.text = Score.ToString();
    }

    void CheckPoints()
    {
        if (Score >= maxScore) 
        {
            PlayerWin();
        }

        if (player.GetComponent<PlayerController>().cHP <1)
        {PlayerDied();}
    }

    void PlayerDied ()
    {
        DeadGame.enabled = true;
    }

    void PlayerWin ()
    {
        EndGame.enabled = true;
    }


}
