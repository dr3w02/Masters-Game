using System.Collections;
using UnityEngine;

public class W_WaypointMover : MonoBehaviour
{ 
    [SerializeField] public WayPoints _wayPoints;
    [SerializeField] private NPCSTATS _npcStatsSource;
    [SerializeField] private PathPicker _pathPicker;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float distanceThreshold = 0.1f;
    [SerializeField] private W_NPCPicker _npcPicker;

    private bool initialized = false;

    public bool sister;

    public FadeGhosts _fadeGhosts;


    public AudioSource ghostChuckle;
  
    public void Initialize(WayPoints wayPoints, W_NPCPicker npcPicker, int ghostSpeed, PathPicker pathPicker)
    {
        _wayPoints = wayPoints;
        _npcPicker = npcPicker;
        _pathPicker = pathPicker;
        moveSpeed = ghostSpeed;
        initialized = true;
    }

    private void OnEnable()
    {
        
    
        ghostChuckle = GetComponent<AudioSource>();
       
        Debug.Log("zz." + _wayPoints);

        if (!initialized)
        {

            _wayPoints = _pathPicker._wayPoints;
        }



        ResetToStart();

        Debug.Log("gv.currentwaypoint");
    }

    private void OnDisable()
    {
        _wayPoints = null;
        initialized = false;
    }


    private void ResetToStart()
    {

        // reset transform
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        if (_wayPoints == null)
        {

            return;
        }


        _wayPoints.currentWaypoint = _wayPoints.transform.GetChild(0);
        Debug.Log("zz.checking paypoints" + _wayPoints.currentWaypoint);
        transform.position = _wayPoints.currentWaypoint.position;


        Transform next = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
        if (next != null)
        {
            transform.LookAt(next.position);

        }
    }

    private void Update()
    {
        Movement();




    }

    private void Movement()
    {

        if (_wayPoints == null)
        {
            return;
        }

        if (_wayPoints.currentWaypoint == null)
        {

            return;
        }


        transform.position = Vector3.MoveTowards(transform.position, _wayPoints.currentWaypoint.position, moveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, _wayPoints.currentWaypoint.position) < distanceThreshold)
        {
           

            if (_wayPoints.currentWaypoint.GetSiblingIndex() == _wayPoints.transform.childCount - 1)
            {
                // reached the end!


                ghostChuckle.Play();

                if (sister)
                { 
                    _fadeGhosts.sister = true;
                    
                    _fadeGhosts.TriggerFade();
                }
                else
                {
                    _fadeGhosts.window = true;
        
                    _fadeGhosts.TriggerFade();
                }

               
                return;
            }

            Transform next = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
           
            if (next == null)
            {

                return;
            }


            _wayPoints.currentWaypoint = next;

            Transform upcoming = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
            Debug.Log("zz.checking paypoints" + next);

            if (upcoming != null)
            {
                transform.LookAt(upcoming.position);
            }
            else
            {
                transform.LookAt(_wayPoints.currentWaypoint.position);
            }

           

           


           
        }
    }


}



