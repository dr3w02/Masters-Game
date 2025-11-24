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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _npcHallwayGhost1 = new npcStatistics("Poltergeist", 1, 15, Poltergeist);
        _npcHallwayGhost2 = new npcStatistics("Residual", 2,  5, Residual);
        _npcHallwayGhost3 = new npcStatistics("Spirit", 1, 10, Spirit);
        _npcHallwaySister = new npcStatistics("H_Sister", 2, +5, H_Sister);

        // Window
        _npcWindowGhost1 = new npcStatistics("Mimic", 3, 5, Mimic);
        _npcWindowGhost2 = new npcStatistics("Demon", 1, 20, Demon);
        _npcWindowSister = new npcStatistics("W_Sister", 2, +2, W_Sister); // make this gradiual the longer they look 


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
    public int ghostSpeed;

    //Ghost Damage
    public int sanityAmount;

    public GameObject ghostPrefab;

    public npcStatistics(string type, int speed, int sanity, GameObject objGhost)
    {
        this.ghostType = type;

        this.ghostSpeed = speed;

        this.sanityAmount = sanity;

        this.ghostPrefab = objGhost;
    }

   
}



