using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_NPCPicker : MonoBehaviour
{

    public NPCSTATS _npcStatsSource;
    public SanityScore _sanityScore;
    public PathPicker _pathPicker;

    public bool resetwayPointMover;

  


    public npcStatistics chosen;
    public int randomIndex;

    public bool isPickingActive = true;



    [SerializeField] private float delayBeforePick = 1.5f;

    public void Awake()
    {
        _npcStatsSource = GetComponent<NPCSTATS>();
        _sanityScore = FindFirstObjectByType<SanityScore>();
        


    }
    private void Start()
    {
      

    }


    public IEnumerator StartNPCPicker(PathPicker pathPicker)
    {
        yield return new WaitForSeconds(delayBeforePick);


        var windowGhosts = _npcStatsSource.windowGhosts;

        if (windowGhosts == null || windowGhosts.Count == 0) yield break;


         chosen = null;

        if (chosen == null)
        {
            int index = Random.Range(0, windowGhosts.Count);
            chosen = windowGhosts[index];
            Debug.Log("Picked WINDOW ghost: " + chosen.ghostType);


         
            chosen.ghostPrefab.SetActive(true);

        }


        Debug.Log("NPCPicked");
        //Spawn ghost and move along path 

    }
  

    public void EndOfPath()
    {

        if(chosen == null)
        {
            return;
        }

        chosen.ghostPrefab.SetActive(false);
        Debug.Log("End Of Path");
        _sanityScore.sanity -= 5;

    }
    public void SisterEndOfPath()
    {

        if (chosen == null)
        {
            return;
        }
        chosen.ghostPrefab.SetActive(false);
        Debug.Log("End Of Path");
        _sanityScore.sanity += 5;

    }

    public void LookedAt()
    {
        if (chosen == null)
        {
            return;
        }

        chosen.ghostPrefab.SetActive(false);
    }

}
