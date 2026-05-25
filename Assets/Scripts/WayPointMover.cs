using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    [SerializeField] public WayPoints _wayPoints;
    [SerializeField] private NPCSTATS _npcStatsSource;
    [SerializeField] private PathPicker _pathPicker;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float distanceThreshold = 0.1f;
    [SerializeField] private NPCPicker _npcPicker;

    private bool initialized = false;

    public bool ghostEnded;
    public FadeGhosts fadeGhosts;

    public AudioClip ghostChuckle;
    private bool chucklePlayed = false;

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
        ghostEnded = false;
        chucklePlayed = false;
        // reset transform
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        transform.rotation = _wayPoints.currentWaypoint.rotation;

        if (_wayPoints == null)
        {
            Debug.LogWarning("waypointmovernull!");
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

        if (_wayPoints == null || _wayPoints.currentWaypoint == null || ghostEnded)
            return;

      


        transform.position = Vector3.MoveTowards(transform.position, _wayPoints.currentWaypoint.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _wayPoints.currentWaypoint.rotation, 50f * Time.deltaTime);

        if (Vector3.Distance(transform.position, _wayPoints.currentWaypoint.position) < distanceThreshold)
        {
            // Reached the last waypoint
            if (_wayPoints.currentWaypoint.GetSiblingIndex() == _wayPoints.transform.childCount - 1)
            {
                Debug.Log("WayPointMover: End of path reached.");
                ghostEnded = true;

                if (ghostChuckle != null)
                    AudioSource.PlayClipAtPoint(ghostChuckle, transform.position);


                if (fadeGhosts != null)
                {
                    fadeGhosts.TriggerFade();
                }

              
                if (_npcPicker != null)
                {
                    _npcPicker.EndOfPath();
                }
                else
                {
                    Debug.LogWarning("WayPointMover: _npcPicker is null, cannot call EndOfPath!");
                }

                return;
            }

            Transform next = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
            _wayPoints.currentWaypoint = next;

            Debug.Log("Moving to waypoint: " + next);

            Transform upcoming = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
            if (upcoming != null)
            {
                transform.LookAt(upcoming.position);
            }
        }
    }

}