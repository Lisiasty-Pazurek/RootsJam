using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSorting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;
    }


}
