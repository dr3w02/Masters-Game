using System.Collections;
using System.Numerics;
using UnityEngine;


public class NPCPicker : MonoBehaviour
{
    public NPCSTATS _npcStatsSource;
    public SanityScore _sanityScore;
    public PathPicker _pathPicker;



    public npcStatistics chosen;
    public int randomIndex;

    public bool isPickingActive = true;

    public bool doorClosed;

    [SerializeField] private float delayBeforePick = 1.5f;

    public void Awake()
    {
        _npcStatsSource = GetComponent<NPCSTATS>();
        _sanityScore = FindFirstObjectByType<SanityScore>();
        
  

    }
    private void Start()
    {
        NPCSTATS stats = FindFirstObjectByType<NPCSTATS>();

    }

   

    public IEnumerator StartNPCPicker(PathPicker pathPicker)
    {
        yield return new WaitForSeconds(delayBeforePick);


        var hallwayGhosts = _npcStatsSource.hallwayGhosts;

        if (hallwayGhosts == null || hallwayGhosts.Count == 0) yield break;

        chosen = null;

        if (chosen == null)
        {
            int index = Random.Range(0, hallwayGhosts.Count);
            chosen = hallwayGhosts[index];
            Debug.Log("Picked hallway ghost: " + chosen.ghostType);

            
            chosen.ghostPrefab.SetActive(true);

        }
       

        Debug.Log("NPCpICKED");
        //Spawn ghost and move along path 
    }



    public void EndOfPath() //maybe this should be here 
    {
            _sanityScore.sanity -= chosen.sanityAmount;
            //_pathPicker.PathChosen();
            Debug.Log("Sanity score" + _sanityScore.sanity);
            Debug.Log("Removed");
          
     
    }
    


}



   









