using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.GRLO
{
    class MoveToSelectedItem : IState
    {

        private readonly WoodcutterBehavior _woodcutter;
        private readonly NavMeshAgent _navMeshAgent;

        private Vector3 _lastPosition = Vector3.zero;
        public float TimeStuck;

        public MoveToSelectedItem(WoodcutterBehavior woodcutter, NavMeshAgent navMeshAgent)
        {
            _woodcutter = woodcutter;
            _navMeshAgent = navMeshAgent;
            StateName = "MoveToSelectedItem";
        }

        public string StateName { get; set; }

        public void OnEnter()
        {
            TimeStuck = 0f;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_woodcutter.TargetItem.transform.position);
            _woodcutter.TargetItem.ReservedFor = _woodcutter.Id;
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Entered: " + StateName);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            _woodcutter.TargetItem.ReservedFor = -1;
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Left: " + StateName);
        }

        public void Tick()
        {
            if (Vector3.Distance(_woodcutter.transform.position, _lastPosition) <= 0f)
                TimeStuck += Time.deltaTime;

            _lastPosition = _woodcutter.transform.position;
        }
    }
}
