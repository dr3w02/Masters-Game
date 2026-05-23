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


    public AudioSource ghostChuckle;
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

        // reset transform
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        transform.rotation = _wayPoints.currentWaypoint.rotation;

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

    public FadeGhosts fadeGhosts;
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _wayPoints.currentWaypoint.rotation, 50f * Time.deltaTime);

        if (Vector3.Distance(transform.position, _wayPoints.currentWaypoint.position) < distanceThreshold)
        {
            
            
            if (_wayPoints.currentWaypoint.GetSiblingIndex() == _wayPoints.transform.childCount - 1)
            {
                Debug.Log("EndOfPath");
                ghostChuckle.Play();
                fadeGhosts.TriggerFade();
                //_npcPicker.EndOfPath();
                //gameObject.SetActive(false);
                return;
            }


            Transform next = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
            

            _wayPoints.currentWaypoint = next;
            Debug.Log("zz.checking paypoints" + next);

            Transform upcoming = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
           
            if (upcoming != null)
            {
                transform.LookAt(upcoming.position);
            }
         


        }
    }
}