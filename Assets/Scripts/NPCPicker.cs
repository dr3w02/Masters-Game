using JetBrains.Annotations;
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

    [Header("Spawn Control")]
    public bool isGhostActive = false;

    private int lastPickedIndex = -1;


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
     public void Update()
    {
        if (_sanityScore.sanity <= 0 && chosen != null && chosen.ghostPrefab != null)
        {
            chosen.ghostPrefab.SetActive(false);
            isGhostActive = false;
        }
    }
   

    public IEnumerator StartNPCPicker(PathPicker pathPicker)
    {

        if (isGhostActive)
        {
            Debug.Log(" A ghost is already active.");
            yield break;
        }

        isGhostActive = true;

        yield return new WaitForSeconds(delayBeforePick);


        var hallwayGhosts = _npcStatsSource.hallwayGhosts;


        if (hallwayGhosts == null || hallwayGhosts.Count == 0)
        {
            isGhostActive = false;
            yield break;
        }

        chosen = null;

        if (chosen == null)
        {
            int index = Random.Range(0, hallwayGhosts.Count);

            if (hallwayGhosts.Count > 1)
            {
                while (index == lastPickedIndex)
                {
                    index = Random.Range(0, hallwayGhosts.Count);
                }
            }


            npcStatistics temporaryChosen = hallwayGhosts[index];
            

            if (temporaryChosen.sisterGhost && sisterSpawnState.sisterGhostActive)
            {

                pathPicker.PathChosen();
                Debug.Log("URDONEhallway");
                isGhostActive = false;
                yield break;
            }

            lastPickedIndex = index;
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

    IEnumerator WaitBeforeSister(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (sisterSpawnState != null)
        {
            sisterSpawnState.sisterGhostActive = false;
            sisterSpawnState.StartHallwaySpawnTimer();
        }


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

        isGhostActive = false;

        if (chosen.sisterGhost)
        {
            StartCoroutine(WaitBeforeSister(10f));
        }
        if (sisterSpawnState != null)
        {
            sisterSpawnState.StartHallwaySpawnTimer();
        }

    }
}



   









