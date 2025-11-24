using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathPicker : MonoBehaviour
{

    public bool isInUse = false;

    public string targetTag = "Pathes";
    public List<GameObject> H_Pathes;
    [SerializeField]
    private List<GameObject> recentlyUsedPathes;


    public float intensity = 1;

  
    public WayPoints wayPoints;
    [SerializeField]
    public NPCPicker _npcPicker;


    private void Awake()
    {
        _npcPicker = FindFirstObjectByType<NPCPicker>();

        H_Pathes = new List<GameObject>();
        recentlyUsedPathes = new List<GameObject>();

        // Find all objects with the target tag
        GameObject[] allPathes = GameObject.FindGameObjectsWithTag(targetTag);
        H_Pathes.AddRange(allPathes);




    }


    public void Start()
    {
        PathChosen();
        if (wayPoints == null)
        {

            Debug.Log("NotAcessable"); // Access a public variable
        }

    }

    public void Update()
    {
       

        if (!isInUse)
        {
            wayPoints = null;
            PathChosen();

        }

        if (H_Pathes.Count == 0)
        {

            H_Pathes.Clear();

            H_Pathes.AddRange(recentlyUsedPathes);

            recentlyUsedPathes.Clear();

        }

    }

    // picks the path out of the allowed pathes to pick and move it to another list where it cant be picked twice ALL WORKS 
    public void PathChosen()
    {
       
        
        if (H_Pathes.Count == 0)
        {
            Debug.LogWarning("No paths left to choose from.");
            return;
        }
        
        wayPoints = FindFirstObjectByType<WayPoints>();

        //picking a random path point for it 

        int randomPath = Random.Range(0, H_Pathes.Count);

        wayPoints = H_Pathes[randomPath].GetComponent<WayPoints>();
 

        //set inital postion to the first waypoint
        wayPoints.currentWaypoint = wayPoints.GetNextWaypoint(wayPoints.currentWaypoint);
        transform.position = wayPoints.currentWaypoint.position;

        //Set the next waypoint target
        wayPoints.currentWaypoint = wayPoints.GetNextWaypoint(wayPoints.currentWaypoint);

        transform.LookAt(wayPoints.currentWaypoint);

        recentlyUsedPathes.Add(H_Pathes[randomPath]); //when the path is over put pack on list
        H_Pathes.Remove(H_Pathes[randomPath]);

        isInUse = true;
        _npcPicker.StartNPCPicker();

    }
 



   
}
