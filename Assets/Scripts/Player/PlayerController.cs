using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 3f;
    public float currentSpeed = 3f;
    [SerializeField]
    public int cHP = 5;

    [SerializeField]
    public float runTime = 3f;
    [SerializeField]
    public float runTimeMax = 3f;

    [SerializeField]
    public float restTime = 0f;
    [SerializeField]
    public float restTimeMax = 3f;
    private bool resting;

    public float invincibleTime = -101f;
    public Animator cAnimator;
    public SpriteRenderer cRenderer;
    public string aState;

    public float pickUpTime = 1f;
    public float blockMoveTime;
    [SerializeField]
    public Transform itemSlot;

    public GameObject carriedItem = null;

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
        pickUpTime -= Time.deltaTime;
        blockMoveTime -= Time.deltaTime;
        if (invincibleTime > -100)
            invincibleTime -= Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal < 0) { cRenderer.flipX = true; }
        else { cRenderer.flipX = false; }
        Animate();

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = moveDirection.normalized * currentSpeed * Time.deltaTime;

        agent.Move(moveDirection);

        if (invincibleTime > 0)
        {
            currentSpeed = speed * 3f;
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            restTime = restTimeMax;
        }
        else
        {
            if (invincibleTime > -100)
            {
                this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
                currentSpeed = speed;
                invincibleTime = -101;
            }
            if (blockMoveTime > 0)
            {
                currentSpeed = speed * 0.05f;

            }
            else
            {
                if (currentSpeed < speed) { currentSpeed = speed; }
                if (Input.GetKeyDown(KeyCode.LeftShift) && carriedItem == null && runTime > 0f)
                {
                    currentSpeed = speed * 1.8f;
                    resting = false;
                }
                if (Input.GetKeyUp(KeyCode.LeftShift) || carriedItem != null || runTime < 0)
                {
                    resting = true;
                    currentSpeed = speed;
                }
                if (resting)
                {
                    if (runTime < runTimeMax)
                    {
                        if (restTime < restTimeMax)
                            restTime += Time.deltaTime;
                        if (restTime >= restTimeMax)
                        {
                            restTime = 0f;
                            runTime = runTimeMax;
                        }
                    }
                }
                else
                {
                    runTime -= Time.deltaTime;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.F) && carriedItem != null && pickUpTime <= 0)
        {
            carriedItem.GetComponent<Interactable>().PutDown(this);
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



    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.GetComponent<WoodcutterBehavior>() != null)
        {
            Debug.Log("Collision with : " + other.gameObject.name + " Jebłem to jebłem");
            invincibleTime = 2f;
            cHP--;
        }
    }

    
}