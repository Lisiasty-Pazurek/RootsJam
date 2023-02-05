using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ForWoodcutter : MonoBehaviour
{
    [SerializeField]
    public string woodcutterItemType = "";
    [SerializeField]
    public string woodcutterItemParam = "";
    [SerializeField]
    public string woodcutterWorkType = "";
    [SerializeField]
    public GameObject WorkRaletedPrefab;
    [SerializeField]
    public float TimeBeforeWorkDone = 1f;
    [SerializeField]
    public int ReservedFor { get; set; } = -1;

    public void pickMe()
    {
        Destroy(this.gameObject);
    }

    public void RootIt()
    {

    }
}

