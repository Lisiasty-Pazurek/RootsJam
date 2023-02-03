using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool canInteract;
    bool itemState = false;

    public void Interact ()
    {
        if (!canInteract) return;
        if (!itemState) itemState = true; 
    }
}
