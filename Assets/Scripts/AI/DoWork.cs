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
            WorkDoneTime = Time.time + 1f;
            if (_woodcutter.ShowDebugMsgs)
                Debug.Log("Entered: " + StateName);
        }

        public void OnExit()
        {
            _woodcutter.WorkDone = false;
            if (_woodcutter.ShowDebugMsgs)
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
