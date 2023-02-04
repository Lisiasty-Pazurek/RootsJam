using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 3f;
    public Animator cAnimator;
    public SpriteRenderer cRenderer;
    public string aState; 

    // Esay toggle in editor to swap between click to move and wasd movement
    [SerializeField] public bool clickToMove;

    public void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        cAnimator = this.GetComponent<Animator>();
        cRenderer = this.GetComponentInChildren<SpriteRenderer>();

    }

    void Update()
    {
        // Click to move movement
        // if (clickToMove == true)
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         agent.destination = hit.point;
        //     }
        // }

        // WASD movement
        if (clickToMove == false) {
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
 //           if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Horizontal") != 0) {aState == "isWalking";}
            if (horizontal < 0 ) {cRenderer.flipX = true;}
            else {cRenderer.flipX = false;}
            Animate();

            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
            moveDirection = moveDirection.normalized * speed * Time.deltaTime;

            agent.Move(moveDirection);   
        }

    }


    void Animate() 
    {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                cAnimator.SetBool("isWalking", true);
            }
            else cAnimator.SetBool("isWalking", false);
    }

    // private void OnCollisionEnter(Collision other) 
    // {
    //     Debug.Log("Collision with : " + other.gameObject.name );
    // }
}