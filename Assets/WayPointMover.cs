using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    public bool isInUse = false;

    public WayPoints wayPoints; // fIX THIS 

    [Range(0f, 10f)]
    [SerializeField] private float moveSpeed = 2f;

    

    [SerializeField] private float distanceThreshold = 0.1f;



    public string targetTag = "Pathes";
    public List<GameObject> H_Pathes;

    //The Current waypoint target that the object is moving towards
    public Transform currentWaypoint;

    private void Awake()
    {
        
        H_Pathes = new List<GameObject>();

        // Find all objects with the target tag
        GameObject[] allPathes = GameObject.FindGameObjectsWithTag(targetTag);
        H_Pathes.AddRange(allPathes);

       
    }

    public void Start()
    {
        if (!isInUse)
        {
            getPath();
        }
        
    }

    public void getPath()
    {

        if (!isInUse)
            {
                isInUse = true;

                //picking a random path point for it 

                int randomPath = Random.Range(0, H_Pathes.Count);

                wayPoints = H_Pathes[randomPath].GetComponent<WayPoints>();

                //set inital postion to the first waypoint
                currentWaypoint = wayPoints.GetNextWaypoint(currentWaypoint);
                transform.position = currentWaypoint.position;

                //Set the next waypoint target
                currentWaypoint = wayPoints.GetNextWaypoint(currentWaypoint);

                transform.LookAt(currentWaypoint);
                isInUse = false;
                return;
            }

      
        Debug.LogWarning("All pathes are currently being used");
        return;
    }

    void Update()
    {
       
        Movement();
       

        if (isInUse)
        {
            Debug.Log("No More Avalible Pathes");

        }
        else
        {
            
        }
    }

    private void Movement()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // if the emeny is close to the way point it goes to the next one 
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = wayPoints.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);

        }
    }


   


}


