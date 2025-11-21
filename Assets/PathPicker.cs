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

    [SerializeField]
    private WayPoints wayPoints; // fIX THIS 
    public NPCPicker npcPicker;

    private void Awake()
    {
       

        H_Pathes = new List<GameObject>();
        recentlyUsedPathes = new List<GameObject>();

        // Find all objects with the target tag
        GameObject[] allPathes = GameObject.FindGameObjectsWithTag(targetTag);
        H_Pathes.AddRange(allPathes);




    }


    public void Start()
    {

        if (wayPoints == null)
        {
            
            Debug.Log("NotAcessable"); // Access a public variable
        }

    }

    public void Update()
    {
        if (npcPicker.picked)
        {

            wayPoints = GetComponent<WayPoints>();
            StartCoroutine(PathSelector());
            npcPicker.picked = false;
        }
    }
    private IEnumerator PathSelector()
    {
        while (true)
        {
            getPath();

            yield return new WaitForSeconds(intensity);
            Debug.Log("LOPPPOJGN");

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
        Debug.Log("pickinh path");
        //set inital postion to the first waypoint
        wayPoints.currentWaypoint = wayPoints.GetNextWaypoint(wayPoints.currentWaypoint);
        transform.position = wayPoints.currentWaypoint.position;

        //Set the next waypoint target
        wayPoints.currentWaypoint = wayPoints.GetNextWaypoint(wayPoints.currentWaypoint);

        transform.LookAt(wayPoints.currentWaypoint);

        recentlyUsedPathes.Add(H_Pathes[randomPath]); //when the path is over put pack on list





    }


    
}
