using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject player;

    public int Score = 0;
    [SerializeField] public Text hpText;
    [SerializeField] public Text Points;

    public int hearts = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        ChangeHP();
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
}
