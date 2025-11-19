using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class NPCPicker : MonoBehaviour
{
    [Header("Hallway NPCS")]
    //Collecting all the npcs for the hallway
    [SerializeField]
    private string H_targetTag = "H_NPC";

    [SerializeField]
    private List<GameObject> H_NPC;


    //Collecting all the npcs for the Window
    [Header("Window NPCS")]
    [SerializeField]
    private string W_targetTag = "W_NPC";

    [SerializeField]
    private List<GameObject> W_NPC;

    [SerializeField]
    private bool picked;

    void Awake()
    {
       //INTITIALISE HALLWAY GHOSTS INTO LIST
        H_NPC = new List<GameObject>();

        // Find all objects with the target tag
        GameObject[] HallNPCS = GameObject.FindGameObjectsWithTag(H_targetTag);
        H_NPC.AddRange(HallNPCS);


        //INTITIALISE WINDOW GHOSTS INTO LIST
        W_NPC = new List<GameObject>();

        // Find all objects with the target tag
        GameObject[] WINDNPCS = GameObject.FindGameObjectsWithTag(W_targetTag);
        W_NPC.AddRange(WINDNPCS);



    }

    public void Update()
    {
        if (!picked)
        {
            Pick();
        }
        else
        {
            return;
        }
    }

    public void Pick()
    {
        Debug.Log("NPCPicked");
        int randomIndex = Random.Range(0, H_NPC.Count);
        GameObject clone = Instantiate(H_NPC[randomIndex], transform.position, Quaternion.identity);
        clone.SetActive(true);
        clone.AddComponent<WayPointMover>();
        picked = true;

    }
 
}


    

