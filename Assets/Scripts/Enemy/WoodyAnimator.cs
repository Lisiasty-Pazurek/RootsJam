using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WoodyAnimator : MonoBehaviour
{
    private Animator eAnimator;
    private NavMeshAgent eAgent;
    private SpriteRenderer eRenderer;
    private WoodcutterBehavior woodcutterState;

    [SerializeField] public List<SpriteRenderer> carriedObject;
    [SerializeField] public GameObject Wood;
    [SerializeField] public GameObject Axe;
    [SerializeField] public GameObject Plank;

    // Start is called before the first frame update
    void Start()
    {
        eAnimator = GetComponentInChildren<Animator>();
        eAgent = GetComponent<NavMeshAgent>();
        eRenderer = GetComponentInChildren<SpriteRenderer>();
        woodcutterState = GetComponent<WoodcutterBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        eAnimator.SetBool("isWalking", eAgent.hasPath);


        if (eAgent.destination != null)
        eRenderer.flipX = (eAgent.destination.x < this.transform.position.x);

        if (woodcutterState.StateName == "DoWork")
        {
            eAnimator.SetBool("isWalking", false);
            eAnimator.SetBool("isWorking", true);
        }
        else eAnimator.SetBool("isWorking", false);

        carriedObjectHandler();

    }

    public void carriedObjectHandler ()
    {
        Axe.SetActive(woodcutterState._haveAxe);
        Wood.SetActive(woodcutterState._haveWoodForSaw || woodcutterState._haveWoodForStock);
        Plank.SetActive(woodcutterState._plankNo > 0);

    }
}
