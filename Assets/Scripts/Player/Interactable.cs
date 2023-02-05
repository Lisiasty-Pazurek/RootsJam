using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(ForWoodcutter))]
public class Interactable : MonoBehaviour
{
    bool canInteract = true;
    // bool itemState = false;

    [SerializeField] bool canBeCarried = false;
    [SerializeField] GameObject rootsOverlay = null;

    [SerializeField] AudioClip laughSound ;


    public void Start ()
    {
        laughSound = Resources.Load<AudioClip>("Audio/evilLaugh2.mp3");
    }


    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player" && Input.GetKeyUp(KeyCode.F)) 
        { 
            Interact(other);
        }

        else return;
        
    }

    public void Interact (Collider other)
    {      
        if  (other.GetComponent<PlayerController>().carriedItem == null && this.canBeCarried)
        {
            PickUp(other);
        }

        else 
        {
            PlantRoots();
            other.GetComponent<PlayerController>().blockMoveTime = 2f;
//            other.GetComponent<AudioSource>().Play(Resources.Load<AudioClip>("Audio/evilLaugh2.mp3"));
        }

    }

    public void PickUp (Collider other)
    {
        this.transform.SetParent(other.GetComponent<PlayerController>().itemSlot);
        this.transform.position = other.GetComponent<PlayerController>().itemSlot.transform.position;
        other.GetComponent<PlayerController>().carriedItem = this.gameObject;
        other.GetComponent<PlayerController>().pickUpTime = 1f;
        this.GetComponent<ForWoodcutter>().ReservedFor = 999;
        
    }

    public void PutDown (PlayerController player)
    {
        this.transform.SetParent(GameObject.Find("CarryOn").transform);
        this.transform.position = player.transform.position;
        player.carriedItem = null;
       ForWoodcutter fw = this.GetComponent<ForWoodcutter>();
       fw.ReservedFor = -1;
    //    fw.RootIt();

    }

    public void PlantRoots()
    {
        this.GetComponent<ForWoodcutter>().RootIt();
        RootedHandler(true);
        
    }

    public void RootedHandler (bool state)
    {
        if (this.GetComponent<ForWoodcutter>().rooted)
        {
            rootsOverlay.SetActive(state);
        }
    }

    void Update ()
    {
        if (canBeCarried == false)
        rootsOverlay.SetActive(this.GetComponent<ForWoodcutter>().rooted);

    }

}
