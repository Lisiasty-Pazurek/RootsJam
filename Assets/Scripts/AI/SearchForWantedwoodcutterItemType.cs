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
            StateName = "SearchForWantedwoodcutterItemType";
            _woodcutter.WorkDone = false;
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Entered: " + StateName);
        }

        public void OnExit()
        {
            if (_woodcutter._showDebugMsgs)
                Debug.Log("Left: " + StateName);
        }



        public void Tick()
        {
            if (_woodcutter.WantedWoodcutterItemTypeList?.Count > 0)
            {
                GetWantedItemType();
            }
            else
                _woodcutter.WantedwoodcutterItemType = "Patrol";
            _woodcutter.TargetItem = ChooseOneOfTheNearestItems(pickFromNearest);
        }

        private void GetWantedItemType()
        {
            bool nextItemAfterThis = true;

            if (_woodcutter._haveWoodForStock)
            {
                _woodcutter.WantedwoodcutterItemType = "Woodpile";
                return;
            }
            if (_woodcutter._haveWoodForSaw)
            {
                _woodcutter.WantedwoodcutterItemType = "Saw";
                return;
            }
            if (_woodcutter._plankNo > 0)
            {
                ForWoodcutter found;
                found = UnityEngine.Object.FindObjectsOfType<ForWoodcutter>()
                    .OrderBy(t => Vector3.Distance(_woodcutter.transform.position, t.transform.position))
                    .Where(t => (t.ReservedFor < 0 && t.woodcutterItemType == "Plank"))
                    .Take(pickFromNearest)
                    .OrderBy(t => UnityEngine.Random.Range(0, int.MaxValue))
                    .FirstOrDefault();
                if (found == null || _woodcutter._plankNo > 1)
                {
                    _woodcutter.WantedwoodcutterItemType = "PlankBox";
                }
                else
                {
                    _woodcutter.WantedwoodcutterItemType = "Plank";
                }
                return;
            }
            if (_woodcutter.currentItemNo >= 0 && _woodcutter.currentItemNo < _woodcutter.WantedWoodcutterItemTypeList?.Count)
            {
                _woodcutter.WantedwoodcutterItemType = _woodcutter.WantedWoodcutterItemTypeList[_woodcutter.currentItemNo];

            }
            else
            {
                _woodcutter.WantedwoodcutterItemType = _woodcutter.WantedWoodcutterItemTypeList[0];
                _woodcutter.currentItemNo = 0;
            }
            if (_woodcutter.WantedwoodcutterItemType.ToLower().Contains("tree"))
            {
                if (!_woodcutter._haveAxe)
                {
                    nextItemAfterThis = false;
                    _woodcutter.WantedwoodcutterItemType = "Axe";
                }
                if (_woodcutter._haveAxe && !_woodcutter._haveAxeSharp)
                {
                    nextItemAfterThis = false;
                    _woodcutter.WantedwoodcutterItemType = "AxeSharper";
                }
            }
            if (_woodcutter.WantedwoodcutterItemType.ToLower().Contains("wood") && _woodcutter._haveAxe)
            {
                nextItemAfterThis = false;
                _woodcutter.WantedwoodcutterItemType = "AxeRack";
            }

            if (nextItemAfterThis)
                _woodcutter.currentItemNo++;
        }

        private ForWoodcutter ChooseOneOfTheNearestItems(int pickFromNearest)
        {
            ForWoodcutter found;
            found = UnityEngine.Object.FindObjectsOfType<ForWoodcutter>()
                .OrderBy(t => Vector3.Distance(_woodcutter.transform.position, t.transform.position))
                .Where(t => (t.ReservedFor < 0 && t.woodcutterItemType == _woodcutter.WantedwoodcutterItemType))
                .Take(pickFromNearest)
                .OrderBy(t => UnityEngine.Random.Range(0, int.MaxValue))
                .FirstOrDefault();

            if (found == null)
            {
                found = UnityEngine.Object.FindObjectsOfType<ForWoodcutter>()
    .OrderBy(t => Vector3.Distance(_woodcutter.transform.position, t.transform.position))
    .Where(t => (t.woodcutterItemType == _woodcutter.WantedwoodcutterItemType))
    .Take(pickFromNearest)
    .OrderBy(t => UnityEngine.Random.Range(0, int.MaxValue))
    .FirstOrDefault();
            }

            if (found == null)
            {
                _woodcutter.currentItemNo++;
            }

            return found;
        }
    }
}
