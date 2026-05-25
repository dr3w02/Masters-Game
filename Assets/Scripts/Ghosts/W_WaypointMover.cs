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
    private bool endHandled = false;


    public bool sister;

    public FadeGhosts _fadeGhosts;
    public AudioClip ghostChuckle;
  
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
        
    
        
       
        

        if (!initialized)
        {

            _wayPoints = _pathPicker._wayPoints;
        }



        ResetToStart();

    }

    private void OnDisable()
    {
        _wayPoints = null;
        initialized = false;
    }


    private void ResetToStart()
    {
        endHandled = false;
        // reset transform
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        if (_wayPoints == null)
        {
            Debug.LogWarning("_wayPoints is null in ResetToStart");
            return;
        }


        _wayPoints.currentWaypoint = _wayPoints.transform.GetChild(0);
  
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

        if (_wayPoints == null || _wayPoints.currentWaypoint == null || endHandled)
            return;


        transform.position = Vector3.MoveTowards(transform.position, _wayPoints.currentWaypoint.position, moveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, _wayPoints.currentWaypoint.position) < distanceThreshold)
        {
           

            if (_wayPoints.currentWaypoint.GetSiblingIndex() == _wayPoints.transform.childCount - 1)
            {
                // reached the end!

                endHandled = true;

                if (ghostChuckle != null)
                    AudioSource.PlayClipAtPoint(ghostChuckle, transform.position);



                if (_fadeGhosts != null)
                {
                    _fadeGhosts.sister = sister;
                    _fadeGhosts.window = !sister;
                    _fadeGhosts.TriggerFade();
                }
                else
                {
                    Debug.LogWarning("W_WaypointMover: _fadeGhosts is null!");
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



