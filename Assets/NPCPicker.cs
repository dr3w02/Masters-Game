using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class NPCPicker : MonoBehaviour
{

    public List<GameObject> NPCOptions = new List<GameObject>();

    void Awake()
    {
        NPCOptions = new List<GameObject>();

        // chose random and add it the waypoints script to it if it has a script on it chose another one if not then add script 
        //once the waypoint pathh is complete turn off the gsme object asnd remove the script from it 

        // for tags for window npcs and normal npcs 

        
    }
}
