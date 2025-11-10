using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class NPCPicker : MonoBehaviour
{

    public Transform H_NPC;
    [SerializeField]
    public GameObject[] NPCOptions;
    public GameObject[] Pathes;
    //private List<GameObject> NPCOptions = new List<GameObject>();


    [SerializeField]
    private WayPointMover wayPointMover;

   

    public Transform H_pathes;

    public Collider Zones;


    //if path is in use dont re list 
    public bool npcAssigned;
    void Awake()
    {
        foreach (GameObject NPCOptions in NPCOptions)
        {
            NPCOptions.SetActive(false); // Activates each enemy in the array
        }



    }

    public void Start()
    {

        if (!npcAssigned)
        {
            Pick();
        }
        else
        {
            Debug.Log("PathInUse");
            return;
        }
    }
    public void Update()
    {
        Pick();
    }

    public void Pick()
    {

        int randomIndex = Random.Range(0, NPCOptions.Length);
        GameObject clone = Instantiate(NPCOptions[randomIndex], transform.position, Quaternion.identity);
        clone.SetActive(true);
        clone.AddComponent<WayPointMover>();

        npcAssigned = true;


    }

  
      // NPCOptions = new List<GameObject>();

       





       

        //string randomItem = GetRandomItemFromList(NPCOptions);
        //string randomNPC = GetComponent 
       // Debug.Log("Randomly picked item: " + randomItem);

        // chose random and  add it the waypoints script to it if it has a script on it chose another one if not then add script 
        //once the waypoint pathh is complete turn off the gsme object asnd remove the script from it 

        // for tags for window npcs and normal npcs 


    

   
}


    

