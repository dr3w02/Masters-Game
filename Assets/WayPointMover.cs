using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
     NPCSTATS npcStatsSource;

    //The Current waypoint target that the object is moving towards
    public Transform currentWaypoint;


    public WayPoints wayPoints; // fIX THIS 


    [Range(0f, 10f)]
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private float distanceThreshold = 0.1f;


    void Update()
    {
       
        Movement();
       
    }

    private void Movement()
    {
        if (currentWaypoint == null)
        {
            Debug.LogWarning("Nothing here :(");
            return;
        }


        this.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // if the emeny is close to the way point it goes to the next one 
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            Transform next = wayPoints.GetNextWaypoint(currentWaypoint);
          

            if (next == null)
            {
                Destroy(gameObject);

                //make path free again
            }

            currentWaypoint = next;
            transform.LookAt(currentWaypoint);
        }
      
        else
        {
            Debug.Log("Last Checkpoint Reached");
            return;
        }
    }


   


}


