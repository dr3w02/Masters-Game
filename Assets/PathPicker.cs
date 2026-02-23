using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathPicker : MonoBehaviour
{

    [Header("ScriptReference")]
    private SpawnManager _manager;
    public WayPoints _wayPoints;
    [SerializeField]
    public NPCPicker _npcPicker;

    [Header("List")]
    public GameObject PathObject { get; private set; }

    public string targetTag = "Pathes";
    public List<GameObject> H_Pathes;
    [SerializeField]
    private List<GameObject> recentlyUsedPathes;



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
     

        if (_wayPoints == null)
        {

            Debug.Log("NotAcessable"); // Access a public variable
        }
      
        //PathChosen();

    }

  

    // picks the path out of the allowed pathes to pick and move it to another list where it cant be picked twice ALL WORKS 
    public void PathChosen()
    {
       
        
        if (H_Pathes.Count == 0)
        {
            Debug.LogWarning("No paths left to choose from.");
            return;
        }
        
        _wayPoints = FindFirstObjectByType<WayPoints>();

        //picking a random path point for it 

        int randomPath = Random.Range(0, H_Pathes.Count);

        _wayPoints = H_Pathes[randomPath].GetComponent<WayPoints>();
 

        //set inital postion to the first waypoint
        _wayPoints.currentWaypoint = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);
        transform.position = _wayPoints.currentWaypoint.position;

        //Set the next waypoint target
        _wayPoints.currentWaypoint = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);

        transform.LookAt(_wayPoints.currentWaypoint);

        recentlyUsedPathes.Add(H_Pathes[randomPath]); //when the path is over put pack on list
        H_Pathes.Remove(H_Pathes[randomPath]);



        _npcPicker.StartCoroutine(_npcPicker.StartNPCPicker());

    }

   




}
