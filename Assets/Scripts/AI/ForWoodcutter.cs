using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ForWoodcutter : MonoBehaviour
{
    [SerializeField]
    public bool rooted = false;
    [SerializeField]
    public string woodcutterItemType = "";
    [SerializeField]
    public string woodcutterItemParam = "";
    [SerializeField]
    public string woodcutterWorkType = "";
    [SerializeField]
    public GameObject WorkRaletedPrefab;
    [SerializeField]
    public bool GivePoint = false;
    public float TimeBeforeWorkDone
    {
        get
        {
            if (rooted)
                return TimeBeforeWorkDoneRooted;
            else
                return TimeBeforeWorkDoneUnrooted;
        }
    }
    [SerializeField]
    public float TimeBeforeWorkDoneUnrooted = 1f;
    [SerializeField]
    public float TimeBeforeWorkDoneRooted = 4f;
    [SerializeField]
    public int ReservedFor { get; set; } = -1;

    public void pickMe()
    {
        Destroy(this.gameObject);
    }

    public void RootIt(bool root = true)
    {
        rooted = root;
    }
}

