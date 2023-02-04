using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Script.GRLO
{
    class SearchForWantedwoodcutterItemType : IState
    {

        private readonly WoodcutterBehavior _woodcutter;
        private readonly NavMeshAgent _navMeshAgent;

        private int pickFromNearest = 3;

        public SearchForWantedwoodcutterItemType(WoodcutterBehavior woodcutter)
        {
            _woodcutter = woodcutter;
        }

        public string StateName { get; set; } = nameof(SearchForWantedwoodcutterItemType);

        public void OnEnter()
        {
            if (_woodcutter.ShowDebugMsgs)
                Debug.Log("Entered: " + StateName);
        }

        public void OnExit()
        {
            if (_woodcutter.ShowDebugMsgs)
                Debug.Log("Left: " + StateName);
        }



        public void Tick()
        {
            if (_woodcutter.WantedWoodcutterItemTypeList?.Count > 0)
            {
                if (_woodcutter.currentItemNo >= 0 && _woodcutter.currentItemNo < _woodcutter.WantedWoodcutterItemTypeList?.Count)
                {
                    _woodcutter.WantedwoodcutterItemType = _woodcutter.WantedWoodcutterItemTypeList[_woodcutter.currentItemNo];
                    _woodcutter.currentItemNo++;
                }
                else
                {
                    _woodcutter.WantedwoodcutterItemType = _woodcutter.WantedWoodcutterItemTypeList[0];
                    _woodcutter.currentItemNo = 1;
                }
            }
            else
                _woodcutter.WantedwoodcutterItemType = "Axe";
            _woodcutter.TargetItem = ChooseOneOfTheNearestItems(pickFromNearest);
        }


        private ForWoodcutter ChooseOneOfTheNearestItems(int pickFromNearest)
        {
            return UnityEngine.Object.FindObjectsOfType<ForWoodcutter>()
                .OrderByDescending(t => Vector3.Distance(_woodcutter.transform.position, t.transform.position))
                .Where(t => (t.ReservedFor < 0 && t.woodcutterItemType == _woodcutter.WantedwoodcutterItemType))
                .Take(pickFromNearest)
                .OrderBy(t => UnityEngine.Random.Range(0, int.MaxValue))
                .FirstOrDefault();
            //Order by descending weźmie obecnie najdalszą
        }
    }
}
