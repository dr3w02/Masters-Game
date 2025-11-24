using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    [SerializeField] public WayPoints _wayPoints;
    [SerializeField] private NPCSTATS _npcStatsSource;
    [SerializeField] private PathPicker _pathPicker;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float distanceThreshold = 0.1f;
    [SerializeField] private NPCPicker _npcPicker;

    private bool initialized = false;

    public void Initialize(WayPoints wayPoints, NPCPicker npcPicker, int ghostSpeed, PathPicker pathPicker)
    {
        _wayPoints = wayPoints;
        _npcPicker = npcPicker;
        _pathPicker = pathPicker;
        moveSpeed = ghostSpeed;
        initialized = true;
    }

    private void OnEnable()
    {
        _wayPoints = null;

        if (!initialized)
        {
            _npcPicker = FindFirstObjectByType<NPCPicker>();
            _pathPicker = FindFirstObjectByType<PathPicker>();
        }

        _wayPoints = FindFirstObjectByType<WayPoints>();

        ResetToStart();
    }

    private void OnDisable()
    {

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
            Debug.LogWarning($"{name}: _wayPoints is null in ResetToStart(). Did you call Initialize()?");
            return;
        }


        _wayPoints.currentWaypoint = _wayPoints.transform.GetChild(0);

        transform.position = _wayPoints.currentWaypoint.position;


        Transform next = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
        if (next != null)
        {
            transform.LookAt(next.position);
            Debug.Log($"{name}: Looking at next waypoint {next.name}");
        }
    }

    private void Update()
    {
        Movement();

        if(_npcPicker.resetwayPointMover)
        {
            _wayPoints = null;
            _npcPicker.resetwayPointMover = false;
        }
     
    }

    private void Movement()
    {

        if (_wayPoints == null)
        {
            return;
        }

        if (_wayPoints.currentWaypoint == null)
        {
            Debug.LogWarning($"{name}: WayPoints.currentWaypoint is null.");
            return;
        }


        transform.position = Vector3.MoveTowards(transform.position, _wayPoints.currentWaypoint.position, moveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, _wayPoints.currentWaypoint.position) < distanceThreshold)
        {
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


