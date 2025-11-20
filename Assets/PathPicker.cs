using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPicker : MonoBehaviour
{

    public bool isInUse = false;


    public string targetTag = "Pathes";
    public List<GameObject> H_Pathes;

 

    public float intensity = 1;


    public WayPoints wayPoints; // fIX THIS 
    public WayPointMover wayPointMover;

    private void Awake()
    {
        wayPointMover = GetComponent<WayPointMover>();

        H_Pathes = new List<GameObject>();

        // Find all objects with the target tag
        GameObject[] allPathes = GameObject.FindGameObjectsWithTag(targetTag);
        H_Pathes.AddRange(allPathes);


    }


    public void Start()
    {
        

        StartCoroutine(PathSelector());

    }

    private IEnumerator PathSelector()
    {
        while (true)
        {
            getPath();

            yield return new WaitForSeconds(intensity);


        }
        

    }
 
    public void getPath()
    {

        if (H_Pathes.Count == 0)
        {
            Debug.LogWarning("No paths left to choose from.");
            return;
        }

        //picking a random path point for it 

        int randomPath = Random.Range(0, H_Pathes.Count);

            wayPoints = H_Pathes[randomPath].GetComponent<WayPoints>();

            //set inital postion to the first waypoint
            wayPointMover.currentWaypoint = wayPoints.GetNextWaypoint(wayPointMover.currentWaypoint);
            transform.position = wayPointMover.currentWaypoint.position;

            //Set the next waypoint target
            wayPointMover.currentWaypoint = wayPoints.GetNextWaypoint(wayPointMover.currentWaypoint);

            transform.LookAt(wayPointMover.currentWaypoint);
         
            H_Pathes.RemoveAt(randomPath);
           
           
          
        

    }
}
