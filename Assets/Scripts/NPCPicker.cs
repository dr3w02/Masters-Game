using System.Collections;
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

    [Header("SisterSpawned")]
    public SpawnManager sisterSpawnState;
   
    public void Awake()
    {
        _npcStatsSource = GetComponent<NPCSTATS>();
        _sanityScore = FindFirstObjectByType<SanityScore>();

        sisterSpawnState = FindFirstObjectByType<SpawnManager>();

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
            npcStatistics temporaryChosen = hallwayGhosts[index];
            

            if (temporaryChosen.sisterGhost && sisterSpawnState.sisterGhostActive)
            {



                pathPicker.PathChosen();
                Debug.Log("URDONEhallway");

                yield break;
            }

            chosen = temporaryChosen;

            Debug.Log("Picked ghost: " + chosen.ghostType);

            if (chosen.sisterGhost)
            {
                sisterSpawnState.sisterGhostActive = true;
            }

            chosen.ghostPrefab.SetActive(true);

        }
       

        Debug.Log("NPCpICKED");
        //Spawn ghost and move along path 
    }



    public void EndOfPath() //maybe this should be here 
    {
        Debug.Log("eNDoFDpATH");
        if (chosen == null)
        {
            return;
        }

      

        if (chosen.ghostPrefab != null)
        {
            chosen.ghostPrefab.SetActive(false);
        }

        _sanityScore.sanity -= chosen.sanityAmount;

        if (chosen.sisterGhost)
        {
            sisterSpawnState.sisterGhostActive = false;
        }
        if (sisterSpawnState != null)
        {
            sisterSpawnState.StartHallwaySpawnTimer();
        }

    }
}



   









