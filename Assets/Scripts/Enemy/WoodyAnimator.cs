using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WoodyAnimator : MonoBehaviour
{
    private Animator eAnimator;
    private NavMeshAgent eAgent;
    private SpriteRenderer eRenderer;


    // Start is called before the first frame update
    void Start()
    {
        eAnimator = GetComponentInChildren<Animator>();
        eAgent = GetComponent<NavMeshAgent>();
        eRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        eAnimator.SetBool("isWalking", eAgent.hasPath);
        

        if (eAgent.destination != null)
        eRenderer.flipX = (eAgent.destination.x < this.transform.position.x);

    }
}
