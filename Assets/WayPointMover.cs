using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    [SerializeField] public WayPoints wayPoints;

    [Range(0f, 10f)]
    [SerializeField] private float moveSpeed = 5f;

    //The Current waypoint target that the object is moving towards
    private Transform currentWaypoint;

    [SerializeField] private float distanceThreshold = 0.1f;


 

    public string targetTag = "Pathes"; // The tag you want to search for
    public List<GameObject> H_Pathes;

    private void Awake()
    {
        
        H_Pathes = new List<GameObject>();

        // Find all objects with the target tag
        GameObject[] allPathes = GameObject.FindGameObjectsWithTag(targetTag);
        H_Pathes.AddRange(allPathes);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      

        Picked();

  
    }

    public void Picked()
    {
        if (H_Pathes == null || H_Pathes.Count == 0)
        {
            Debug.LogWarning("No paths found with tag: " + targetTag);
            return;
        }

        //picking a random path point for it 

        int randomPath = Random.Range(0, H_Pathes.Count);

        wayPoints = H_Pathes[randomPath].GetComponent<WayPoints>();

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
