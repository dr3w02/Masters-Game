using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    [SerializeField]
    public WayPoints _wayPoints;
    [SerializeField]
    private NPCSTATS _npcStatsSource;

    [SerializeField] private float moveSpeed ;

    [SerializeField] private float distanceThreshold = 0.1f;

    [SerializeField]
    private NPCPicker _npcPicker;


    public void Start()
    {

        _npcPicker = FindFirstObjectByType<NPCPicker>();
        _wayPoints = FindFirstObjectByType<WayPoints>();

        transform.position = _wayPoints.currentWaypoint.position;
        transform.LookAt(_wayPoints.currentWaypoint);

        int moveSpeed = _npcPicker.chosen.ghostSpeed;

        Debug.Log(_wayPoints.currentWaypoint);
        _wayPoints.currentWaypoint = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
       

        //setnextwaypoint target 
        _wayPoints.currentWaypoint = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);


    }
    public void Update()
    {
        Movement();
    }


   
    private void Movement()
    {
        if (_wayPoints.currentWaypoint == null)
        {
            Debug.LogWarning("Nothing here :(");
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _wayPoints.currentWaypoint.position, moveSpeed * Time.deltaTime);

        // if the emeny is close to the way point it goes to the next one 
        if (Vector3.Distance(transform.position, _wayPoints.currentWaypoint.position) < distanceThreshold)
        {
           
            _wayPoints.currentWaypoint = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
            
            transform.LookAt(_wayPoints.currentWaypoint);
        }
    
        else
        {

            return;
        }
    }

}   



