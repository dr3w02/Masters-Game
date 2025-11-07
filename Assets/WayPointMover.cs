using Unity.Collections;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    [SerializeField] private WayPoints wayPoints;

    [Range(0f, 10f)]
    [SerializeField] private float moveSpeed = 5f;

    //The Current waypoint target that the object is moving towards
    private Transform currentWaypoint;

    [SerializeField] private float distanceThreshold = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set inital postion to the first waypoint
        currentWaypoint = wayPoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        //Set the next waypoint target
        currentWaypoint = wayPoints.GetNextWaypoint(currentWaypoint);

        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // if the emeny is close to the way point it goes to the next one 
        if(Vector3.Distance(transform.position,currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = wayPoints.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);

        }
    }
}
