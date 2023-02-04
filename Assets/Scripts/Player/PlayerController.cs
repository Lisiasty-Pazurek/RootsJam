using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 3f;

    // Esay toggle in editor to swap between click to move and wasd movement
    [SerializeField] public bool clickToMove;

    public void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Click to move movement
        if (clickToMove == true)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }

        // WASD movement
        if (clickToMove == false) {
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
            moveDirection = moveDirection.normalized * speed * Time.deltaTime;

            agent.Move(moveDirection);   
        }

    }

    private void OnCollisionEnter(Collision other) 
    {
        //Debug.Log("Collision with : " + other.gameObject.name );
    }
}