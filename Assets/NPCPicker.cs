using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class NPCPicker : MonoBehaviour
{
    public Transform H_NPC;
    [SerializeField]
    private List<GameObject> NPCOptions = new List<GameObject>();


    [SerializeField]
    private List<GameObject> PathesHallway = new List<GameObject>();

    public Transform H_pathes;

    public Collider Zones;


    void Awake()
    {
        NPCOptions = new List<GameObject>();

        foreach (Transform child in H_NPC)
        {
            NPCOptions.Add(child.gameObject);
        }

        PathesHallway = new List<GameObject>();

        foreach (Transform child in H_pathes)
        {
            PathesHallway.Add(child.gameObject);
        }


        // chose random and add it the waypoints script to it if it has a script on it chose another one if not then add script 
        //once the waypoint pathh is complete turn off the gsme object asnd remove the script from it 

        // for tags for window npcs and normal npcs 


    }

    
}
