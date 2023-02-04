using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.GRLO
{
    public class Chase : IState
    {
        private readonly WoodcutterBehavior _woodcutter;
        private readonly NavMeshAgent _navMeshAgent;
        public string StateName { get; set; }
        public Chase(WoodcutterBehavior woodcutter, NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _woodcutter = woodcutter;
            StateName = "Chase";
        }

        public void OnEnter()
        {
            _woodcutter.currentItemNo--;
            if (_woodcutter.currentItemNo < 0)
                _woodcutter.currentItemNo = _woodcutter.WantedWoodcutterItemTypeList.Count - 1;
            _navMeshAgent.enabled = true;
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Entered: " + StateName);
        }

        public void OnExit()
        {
            _woodcutter.LookAround = true;
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Left: " + StateName);
        }

        public void Tick()
        {

            _navMeshAgent.SetDestination(_woodcutter.PlayerLastPosition);
        }
    }
}
