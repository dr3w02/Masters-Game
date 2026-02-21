using System.Collections;
using System.Numerics;
using UnityEngine;


public class NPCPicker : MonoBehaviour
{
    public NPCSTATS _npcStatsSource;
    public SanityScore _sanityScore;
    public PathPicker _pathPicker;

    public bool resetwayPointMover;

    public float intensity;



    public npcStatistics chosen;
    public int randomIndex;

    public bool isPickingActive = true;

    public bool doorClosed;

    [SerializeField] private float delayBeforePick = 1.5f;

    public void Awake()
    {
        _npcStatsSource = FindFirstObjectByType<NPCSTATS>();
        _sanityScore = FindFirstObjectByType<SanityScore>();
        _pathPicker = FindFirstObjectByType<PathPicker>();
  

    }
    private void Start()
    {
        NPCSTATS stats = FindFirstObjectByType<NPCSTATS>();

    }

   

    public IEnumerator StartNPCPicker()
    {
        yield return new WaitForSeconds(delayBeforePick);


        var hallwayGhosts = _npcStatsSource.hallwayGhosts;

        if (hallwayGhosts == null || hallwayGhosts.Count == 0) yield break;

        npcStatistics chosen = null;

        if (chosen == null)
        {
            int index = Random.Range(0, hallwayGhosts.Count);
            chosen = hallwayGhosts[index];
            Debug.Log("Picked hallway ghost: " + chosen.ghostType);

            chosen.ghostPrefab.SetActive(true);

        }
       

        Debug.Log("NPCPicked");
        //Spawn ghost and move along path 
    }



    public void EndOfPath()
    {
            _sanityScore.sanity -= chosen.sanityAmount;
            Debug.Log("Sanity score" + _sanityScore.sanity);
            chosen.ghostPrefab.SetActive(false);
            Debug.Log("Removed");
          
     
    }
    
    
    public void DoorClosed()
    {

        chosen.ghostPrefab.SetActive(false);
       
    }

}



   









