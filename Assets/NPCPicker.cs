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


    [SerializeField] private float delayBeforePick = 1.5f;

    public void Awake()
    {
        _npcStatsSource = FindFirstObjectByType<NPCSTATS>();
        _sanityScore = FindFirstObjectByType<SanityScore>();
        _pathPicker = FindFirstObjectByType<PathPicker>();
  


    }
    private void Start()
    {
        // Start delayed picking
      
    }
    public void StartNPCPicker()
    {
        StartCoroutine(DelayedPick());
    }

    private IEnumerator DelayedPick()
    {
        // Wait for ghosts to finish spawning
        yield return new WaitForSeconds(delayBeforePick);

        PickHallwayGhost();
    }


    public void PickHallwayGhost()
    {
        NPCSTATS stats = FindFirstObjectByType<NPCSTATS>();
     

        var hallwayGhosts = _npcStatsSource.hallwayGhosts;

        if (hallwayGhosts == null || hallwayGhosts.Count == 0)
        {
            Debug.LogWarning("No hallway ghosts found to pick!");
            isPickingActive = false;
            return;
        }

        int index = Random.Range(0, hallwayGhosts.Count);
        chosen = hallwayGhosts[index];
        Debug.Log("Picked hallway ghost: " + chosen.ghostType);

        if (!chosen.ghostPrefab.activeInHierarchy)
        {
            chosen.ghostPrefab.SetActive(true);
        }
        else
        {
            PickHallwayGhost();
        }
 
      _pathPicker.isInUse = false;
        Debug.Log("NPCPicked");
        //Spawn ghost and move along path 


    }


    public void EndOfPath()
    {
        
        _sanityScore.sanity -= chosen.sanityAmount;
        Debug.Log("Sanity score" + _sanityScore.sanity);
        chosen.ghostPrefab.SetActive(false);
        Debug.Log("Removed");
        _pathPicker.isInUse = false;


    }
}



   









