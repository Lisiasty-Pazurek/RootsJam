using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.GRLO
{
    public class DoWork : IState
    {
        private readonly WoodcutterBehavior _woodcutter;
        private float WorkDoneTime;
        public DoWork(WoodcutterBehavior woodcutter)
        {
            _woodcutter = woodcutter;
            StateName = "DoWork";
        }
        public string StateName { get; set; }

        public void OnEnter()
        {
            _woodcutter.WorkDone = false;




            WorkDoneTime = Time.time + _woodcutter.TargetItem.TimeBeforeWorkDone;
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Entered: " + StateName);
        }

        public void OnExit()
        {
            switch (_woodcutter.TargetItem.woodcutterWorkType)
            {
                case "CreateWood":
                    try
                    {
                        Vector3 target = _woodcutter.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1.5f, .5f));
                        _woodcutter.CreateInstance(_woodcutter.TargetItem.WorkRaletedPrefab, target, Quaternion.identity);
                    }
                    catch (Exception ex)
                    {
                    }

                    if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
                    {
                        _woodcutter._haveAxeSharp = false;
                    }

                    break;
                case "PutWood":
                    try
                    {
                        _woodcutter._haveWoodForStock = false;
                        Vector3 target = _woodcutter.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
                        _woodcutter.CreateInstance(_woodcutter.TargetItem.WorkRaletedPrefab, target, Quaternion.identity);
                    }
                    catch (Exception ex)
                    {
                    }

                    if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
                    {
                        _woodcutter._haveAxeSharp = false;
                    }

                    break;
                case "Saw":
                    _woodcutter._haveWoodForSaw = false;
                    try
                    {
                        Vector3 target = _woodcutter.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 2f), 0, UnityEngine.Random.Range(-1.5f, .5f));
                        _woodcutter.CreateInstance(_woodcutter.TargetItem.WorkRaletedPrefab, target, Quaternion.identity);
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                case "PutAxe":
                    try
                    {
                        if (_woodcutter._haveAxe)
                        {

                            _woodcutter._haveAxe = false;
                            Vector3 target = _woodcutter.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
                            GameObject o = _woodcutter.CreateInstance(_woodcutter.TargetItem.WorkRaletedPrefab, target, Quaternion.identity);
                            if (!_woodcutter._haveAxeSharp)
                            {
                                o.GetComponent<ForWoodcutter>().woodcutterItemParam = "needSharper";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    break;
                case "PickWood":
                    if (_woodcutter.TargetItem.woodcutterItemParam == "ForSaw")
                        _woodcutter._haveWoodForSaw = true;
                    else if(_woodcutter.TargetItem.woodcutterItemParam == "Plank")
                        _woodcutter._plankNo++;
                    else
                        _woodcutter._haveWoodForStock = true;
                    _woodcutter.TargetItem.pickMe();
                    break;               
                case "PickAxe":
                    _woodcutter._haveAxe = true;
                    if (_woodcutter.TargetItem.woodcutterItemParam == "NeedSharper")
                        _woodcutter._haveAxeSharp = false;
                    else
                        _woodcutter._haveAxeSharp = true;

                    _woodcutter.TargetItem.pickMe();
                    break;
                case "AxeSharper":
                    _woodcutter._haveAxeSharp = true;
                    break;
                case "PlankBox":
                    _woodcutter._plankNo = 0;
                    break;
                default:
                    break;
            }

            _woodcutter.WorkDone = false;
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Left: " + StateName);
        }

        public void Tick()
        {
            if (Time.time > WorkDoneTime)
            {
                _woodcutter.WorkDone = true;
            }
            else
                _woodcutter.WorkDone = false;
        }
    }
}
