using Assets.Script.GRLO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WoodcutterBehavior : MonoBehaviour
{
    [SerializeField]
    public int Id;
    private StateMachine _stateMachine;

    [SerializeField]
    public ForWoodcutter TargetItem;

    [SerializeField]
    public string StateName;

    [SerializeField]
    public float ChaseTime = 4f;

    [SerializeField]
    public GameObject ChasingGraphicObject;

    public string WantedwoodcutterItemType;
    public bool WorkDone;

    public bool SeePlayer;
    public float SeePlayerLastTime;
    public bool LookAround;
    public Vector3 PlayerLastPosition;

    [SerializeField]
    public bool _haveAxe = true;
    [SerializeField]
    public bool _haveAxeSharp = true;

    [SerializeField]
    public bool _haveWoodForStock = false;

    [SerializeField]
    public bool _haveWoodForSaw = false;

    [SerializeField]
    public int _plankNo = 0;


    Vector3 _lastPosition;
    float GlobalTimeStuck = 0f;

    [SerializeField]
    public List<string> WantedWoodcutterItemTypeList;
    [SerializeField]
    public int currentItemNo = 0;

    [SerializeField]
    public GameObject _player;

    [SerializeField]
    public bool _showDebugMsgs = true;

    float noTargetTimer = 0f;

    NavMeshAgent navMeshAgent;
    private void Awake()
    {
        InitStateMachine();

    }

    private void InitStateMachine()
    {

        _stateMachine = new StateMachine();

        navMeshAgent = GetComponent<NavMeshAgent>();

        var searchForWantedItem = new SearchForWantedwoodcutterItemType(this);
        var moveToSelectedItem = new MoveToSelectedItem(this, navMeshAgent);
        var doWork = new DoWork(this);
        var chase = new Chase(this, navMeshAgent);
        var lookAround = new LookAround(this, navMeshAgent);
        //var harvest = new HarvestResource(this, animator);
        //var returnToStockpile = new ReturnToStockpile(this, navMeshAgent, animator);
        //var placeResourcesInStockpile = new PlaceResourcesInStockpile(this);
        //var flee = new Flee(this, navMeshAgent, enemyDetector, animator, fleeParticleSystem);

        At(searchForWantedItem, moveToSelectedItem, HasTarget());
        At(moveToSelectedItem, searchForWantedItem, StuckForOverASecond());
        At(moveToSelectedItem, doWork, ReachedTargetItem());
        At(doWork, searchForWantedItem, () => WorkDone);
        At(searchForWantedItem, lookAround, () => noTargetTimer > 3f);
        //At(harvest, search, TargetIsDepletedAndICanCarryMore());
        //At(harvest, returnToStockpile, InventoryFull());
        //At(returnToStockpile, placeResourcesInStockpile, ReachedStockpile());
        //At(placeResourcesInStockpile, search, () => _gathered == 0);

        _stateMachine.AddAnyTransition(chase, () => SeePlayer);
        _stateMachine.AddAnyTransition(searchForWantedItem, () => GlobalTimeStuck > 10f);
        //At(chase, lookAround, () => (SeePlayer == false && (SeePlayerLastTime + ChaseTime < Time.time || navMeshAgent.remainingDistance < 0.2f))); //to nie dzia³a
        // At(chase, lookAround, () => (SeePlayer == false && (SeePlayerLastTime + ChaseTime < Time.time))); //to dzia³a
        At(chase, lookAround, () => (SeePlayer == false && (SeePlayerLastTime + ChaseTime < Time.time || (SeePlayerLastTime + 1f < Time.time && navMeshAgent.remainingDistance < 0.2f))));
        At(lookAround, moveToSelectedItem, () => (LookAround == true && SeePlayerLastTime + ChaseTime < Time.time && TargetItem != null));
        At(lookAround, searchForWantedItem, () => (LookAround == true && SeePlayerLastTime + ChaseTime < Time.time && TargetItem == null));

        _stateMachine.SetState(searchForWantedItem);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> HasTarget() => () => TargetItem != null;
        Func<bool> StuckForOverASecond() => () => (moveToSelectedItem.TimeStuck > 10f || TargetItem == null);
        Func<bool> ReachedTargetItem() => () =>
        {
            if (TargetItem != null)
            {
                // Debug.Log(Vector3.Distance(transform.position, TargetAxe.transform.position));
                return Vector3.Distance(transform.position, TargetItem.transform.position) < 0.5f;

            }
            else
                return false;
        };

        //Func<bool> TargetIsDepletedAndICanCarryMore() => () => (Target == null || Target.IsDepleted) && !InventoryFull().Invoke();
        //Func<bool> InventoryFull() => () => _gathered >= _maxCarried;
        //Func<bool> ReachedStockpile() => () => StockPile != null &&
        //                                       Vector3.Distance(transform.position, StockPile.transform.position) < 1f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Id == 2)
        {
            if (_showDebugMsgs)
            {
                Debug.Log("!!!");
            }
        }
        try
        {
            if (_stateMachine == null)
                InitStateMachine();

            _stateMachine.Tick();



            StateName = _stateMachine._currentState.StateName;
            if (_stateMachine._currentState.StateName == "Chase")
                ChasingGraphicObject.SetActive(true);
            else
                ChasingGraphicObject.SetActive(false);

            // if (Vector3.Distance(transform.position, _lastPosition) == 0f) //chyba nie dzia³a
            if (navMeshAgent.enabled && (Vector3.Distance(transform.position, _lastPosition) == 0f) && !SeePlayer)
                GlobalTimeStuck += Time.deltaTime;
            else
                GlobalTimeStuck = 0;

            if (_stateMachine._currentState.StateName == "SearchForWantedwoodcutterItemType")
            {
                noTargetTimer += Time.deltaTime;
            }
            else
            {
                noTargetTimer = 0f;
            }

            _lastPosition = transform.position;
        }
        catch (Exception e)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SeePlayer = true;
            SeePlayerLastTime = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SeePlayer = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SeePlayer = true;
            SeePlayerLastTime = Time.time;
            PlayerLastPosition = other.gameObject.transform.position; //_player.transform.position; //
        }
    }

    public GameObject CreateInstance(GameObject prefab, Vector3 vector3, Quaternion rotation)
    {
        return Instantiate(prefab, vector3, rotation);
    }
    public void DestroyTargerItem()
    {
        try
        {
            Destroy(TargetItem);

        }
        catch (Exception)
        {

        }
    }
}
