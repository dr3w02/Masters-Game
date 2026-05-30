using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathPicker : MonoBehaviour
{

    [Header("ScriptReference")]
    public SpawnManager _manager;
    public WayPoints _wayPoints;
    [SerializeField]
    public NPCPicker _npcPicker;
    public W_NPCPicker _npcWindow;
   

    public string targetTag;
    public List<GameObject> H_Pathes;
    [SerializeField]
    private List<GameObject> recentlyUsedPathes;

 
    private void Awake()
    {
        _npcPicker = GetComponent<NPCPicker>();
        if (_npcPicker == null)
        {
            _npcPicker = FindFirstObjectByType<NPCPicker>();
        }

        _npcWindow = GetComponent<W_NPCPicker>();
        if (_npcWindow == null)
        {
            _npcWindow = FindFirstObjectByType<W_NPCPicker>();
        }


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

        }

        if (!string.IsNullOrEmpty(targetTag))
        {
            GameObject[] allPathes = GameObject.FindGameObjectsWithTag(targetTag);
            H_Pathes.AddRange(allPathes);
        }
        else
        {
         
        }

    }

  

    // picks the path out of the allowed pathes to pick and move it to another list where it cant be picked twice ALL WORKS 
    public void PathChosen()
    {

       

        if (H_Pathes.Count == 0)
        {
            H_Pathes.AddRange(recentlyUsedPathes);
            recentlyUsedPathes.Clear();
        }

        if (targetTag == "Pathes" && _npcPicker != null)
        {
            _npcPicker.chosen = null;
        }
        if (targetTag == "W_Pathes" && _npcWindow != null)
        {
            _npcWindow.chosen = null;
        }



        //picking a random path point for it 

        int randomPath = Random.Range(0, H_Pathes.Count);

        _wayPoints = H_Pathes[randomPath].GetComponent<WayPoints>();

        if (_wayPoints == null)
        {
       
            return;
        }

     
        _wayPoints.currentWaypoint = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);

        if(_wayPoints.currentWaypoint != null)
        {
            transform.position = _wayPoints.currentWaypoint.position;
        }
        else
        {
            
        }
       


        _wayPoints.currentWaypoint = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);

        transform.LookAt(_wayPoints.currentWaypoint);

        _wayPoints.currentWaypoint = _wayPoints.GetNextWaypoint(_wayPoints.currentWaypoint);

        if (_wayPoints.currentWaypoint != null)
        {
           
            if (Vector3.Distance(transform.position, _wayPoints.currentWaypoint.position) > 0.1f)
            {
                transform.LookAt(_wayPoints.currentWaypoint);
            }
        }
        else
        {
            
        }

        recentlyUsedPathes.Add(H_Pathes[randomPath]);
        H_Pathes.Remove(H_Pathes[randomPath]);

        if(targetTag == "Pathes")
        {
            _npcPicker.StartCoroutine(_npcPicker.StartNPCPicker(this));
           
        }


        if (targetTag == "W_Pathes")
        {
            

            _npcWindow.StartCoroutine(_npcWindow.StartNPCPicker(this)); 
        }
      



    }

   




}
