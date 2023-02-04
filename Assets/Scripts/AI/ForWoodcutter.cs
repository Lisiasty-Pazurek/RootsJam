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
    public int ReservedFor { get; set; } = -1;
}

