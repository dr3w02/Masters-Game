using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    private int currentWaypointIndex = 0;

    public void Initialize(WayPoints wayPoints, NPCPicker npcPicker, int ghostSpeed, PathPicker pathPicker)
    {
        _wayPoints = wayPoints;
        _npcPicker = npcPicker;
        _pathPicker = pathPicker;
        moveSpeed = ghostSpeed;
        initialized = true;
        ResetToStart();
    }

    private void OnEnable()
    {
       
        if (!initialized && _pathPicker != null)
        {
            _wayPoints = _pathPicker._wayPoints;
        }

        
        if (_wayPoints == null || _wayPoints.transform.childCount == 0)
        {
            Debug.Log("WayPointMover: Waiting for a path.");
            return;
        }

        ResetToStart();


        
        Debug.Log("WayPointMover.currentwaypoint");
    }

    private void OnDisable()
    {
        
        initialized = false;
    }

    private void ResetToStart()
    {

        if (_wayPoints == null || _wayPoints.transform.childCount == 0)
        {
            return;
        }

        ghostEnded = false;
        chucklePlayed = false;
        currentWaypointIndex = 0;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        Transform firstWaypoint = _wayPoints.transform.GetChild(0);
        transform.position = firstWaypoint.position;
        transform.rotation = firstWaypoint.rotation;
    }


    
    

    private void Update()
    {
        Movement();


    }
    
    private void Movement()
    {
        if (_wayPoints == null || ghostEnded || currentWaypointIndex >= _wayPoints.transform.childCount)
            return;

        Transform targetWaypoint = _wayPoints.transform.GetChild(currentWaypointIndex);
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        direction.y = 0;

        if (direction != Vector3.zero)
        {
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 120f * Time.deltaTime); // Increased speed to 120f for snappier corners
        }

        if (Vector3.Distance(transform.position, targetWaypoint.position) < distanceThreshold)
        {
            if (currentWaypointIndex == _wayPoints.transform.childCount - 1)
            {
                Debug.Log("EndOfPath.");

                if (ghostChuckle != null && !chucklePlayed)
                {
                    chucklePlayed = true;
                    AudioSource.PlayClipAtPoint(ghostChuckle, transform.position);
                }

                if (fadeGhosts != null)
                {
                    fadeGhosts.TriggerFade();

                   
                    StartCoroutine(WaitForFadeAndDespawn());
                }
                else
                {
                    
                    if (_npcPicker != null) _npcPicker.EndOfPath();
                }

                ghostEnded = true;

                

                return;
            }

           
            currentWaypointIndex++;
        }


    }
    private IEnumerator WaitForFadeAndDespawn()
    {
        
        yield return new WaitForSeconds(2f);

       
        if (_npcPicker != null)
        {
            _npcPicker.EndOfPath();
        }
    }



}