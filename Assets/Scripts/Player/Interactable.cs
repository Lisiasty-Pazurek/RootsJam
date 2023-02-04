using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class Interactable : MonoBehaviour
{
    bool canInteract;
    bool itemState = false;

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player" && Input.GetKey("F")) 
        { 
            Interact();
        }

        else return;
        
    }

    public void Interact ()
    {
        if (!canInteract) return;
        if (!itemState) itemState = true; 
    }
}
