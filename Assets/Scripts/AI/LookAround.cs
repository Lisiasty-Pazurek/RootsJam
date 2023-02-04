using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.GRLO
{
    class LookAround : IState
    {
        private readonly WoodcutterBehavior _woodcutter;
        private readonly NavMeshAgent _navMeshAgent;
        float timeToChange;
        float timeToEndLooking;
        int i;

        public string StateName { get; set; }
        public LookAround(WoodcutterBehavior woodcutter, NavMeshAgent navMeshAgent)
        {
            _woodcutter = woodcutter;
            _navMeshAgent = navMeshAgent;
            StateName = "LookAround";
        }

        public void OnEnter()
        {
            i = 0;
            timeToEndLooking = Time.time + UnityEngine.Random.Range(3f, 6f);
            _woodcutter.SeePlayerLastTime = Time.time;
            _navMeshAgent.enabled = true;
            Vector2 rand = new Vector2(UnityEngine.Random.Range(1f, 2f) * UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(1f, 2f) * UnityEngine.Random.Range(-1, 2));
            _navMeshAgent.SetDestination( _woodcutter.transform.position + new Vector3(rand.x,0,rand.y));
            timeToChange = Time.time + UnityEngine.Random.Range(1f, 3f);
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Entered: " + StateName);
        }

        public void OnExit()
        {
            _woodcutter.LookAround = false;
            _navMeshAgent.enabled = false;
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Left: " + StateName);
        }

        public void Tick()
        {
            if (Time.time > timeToChange || _navMeshAgent.remainingDistance<0.5f)
            {
                Vector2 rand = new Vector2(UnityEngine.Random.Range(1f, 2f) * UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(1f, 2f) * UnityEngine.Random.Range(-1, 2));
                _navMeshAgent.SetDestination(_woodcutter.transform.position + new Vector3(rand.x, 0, rand.y));
                timeToChange = Time.time + UnityEngine.Random.Range(1f, 3f);
                i++;
            }
            if (timeToEndLooking < Time.time)
            {
                _woodcutter.LookAround = true;
            }

        }
    }
}
