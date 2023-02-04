using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactable : MonoBehaviour
{
    bool canInteract = true;
    bool itemState = false;

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F)) 
        { 
            Interact(other);
        }

        else return;
        
    }

    public void Interact (Collider other)
    {
//        if (!canInteract) return;
        // if (!itemState) itemState = true; 

        this.transform.SetParent(other.GetComponent<PlayerController>().itemSlot);
        this.transform.position = other.GetComponent<PlayerController>().itemSlot.transform.position;
    }
}
