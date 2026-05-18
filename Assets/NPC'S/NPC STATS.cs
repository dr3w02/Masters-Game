using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class NPCSTATS : MonoBehaviour
{
    private npcStatistics _npcHallwayGhost1;
    private npcStatistics _npcHallwayGhost2;
    private npcStatistics _npcHallwayGhost3;
    private npcStatistics _npcHallwaySister;


    //Window 
    private npcStatistics _npcWindowGhost1;
    private npcStatistics _npcWindowGhost2;
    private npcStatistics _npcWindowSister;


    public GameObject Poltergeist;
    public GameObject Residual;
    public GameObject Spirit;
    public GameObject H_Sister;

    public GameObject Mimic;
    public GameObject Demon;
    public GameObject W_Sister;
   


    public List<npcStatistics> hallwayGhosts = new List<npcStatistics>();
    public List<npcStatistics> windowGhosts = new List<npcStatistics>();

   
    void Start()
    {
        _npcHallwayGhost1 = new npcStatistics("Poltergeist", 0.5f, 15, Poltergeist,false);
        _npcHallwayGhost2 = new npcStatistics("Residual", 0.5f,  5, Residual, false);
        _npcHallwayGhost3 = new npcStatistics("Spirit", 0.5f, 10, Spirit, false);
        _npcHallwaySister = new npcStatistics("H_Sister", 1, +5, H_Sister, true);

        // Window
        _npcWindowGhost1 = new npcStatistics("Mimic", 0.5f, 5, Mimic, false);
        _npcWindowGhost2 = new npcStatistics("Demon", 0.5f, 20, Demon, false);
        _npcWindowSister = new npcStatistics("W_Sister", 1, +2, W_Sister, true); // make this gradiual the longer they look 


        hallwayGhosts.Add(_npcHallwayGhost1);
        hallwayGhosts.Add(_npcHallwayGhost2);
        hallwayGhosts.Add(_npcHallwayGhost3);
        hallwayGhosts.Add(_npcHallwaySister);

        windowGhosts.Add(_npcWindowGhost1);
        windowGhosts.Add(_npcWindowGhost2);
        windowGhosts.Add(_npcWindowSister);

    }



}


[System.Serializable]
public class npcStatistics
{
 

    //Ghost type 
    public string ghostType;

    //Ghost speed
    public float ghostSpeed;

    //Ghost Damage
    public int sanityAmount;

    public GameObject ghostPrefab;

    public bool sisterGhost;

    public npcStatistics(string type, float speed, int sanity, GameObject objGhost, bool sister)
    {
        this.ghostType = type;

        this.ghostSpeed = speed;

        this.sanityAmount = sanity;

        this.ghostPrefab = objGhost;

        this.sisterGhost = sister;
    }

   
}



