using JetBrains.Annotations;
using System.Collections;
using System.Linq;
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
        if (_npcStatsSource == null) _npcStatsSource = GetComponent<NPCSTATS>();
        if (_sanityScore == null) _sanityScore = FindFirstObjectByType<SanityScore>();
        if (sisterSpawnState == null) sisterSpawnState = FindFirstObjectByType<SpawnManager>();

    }
    private void Start()
    {
      

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
                Debug.Log("Sister is active");

                var validGhosts = hallwayGhosts.Where(g => !g.sisterGhost).ToList();
                if (validGhosts.Count > 0)
                {
                    temporaryChosen = validGhosts[Random.Range(0, validGhosts.Count)];
                }
                else
                {
                    
                    isGhostActive = false;
                    yield break;
                }
            }

            lastPickedIndex = index;
            chosen = temporaryChosen;

            Debug.Log("Picked ghost: " + chosen.ghostType);

            if (chosen.sisterGhost)
            {
                sisterSpawnState.sisterGhostActive = true;
            }

            WayPointMover mover = chosen.ghostPrefab.GetComponent<WayPointMover>();

            if (mover != null)
            {
               
                mover.Initialize(pathPicker._wayPoints, this, (int)chosen.ghostSpeed, pathPicker);
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
            chosen.ghostPrefab.SetActive(false);
            StartCoroutine(WaitBeforeSister(2f));
        }
        chosen = null;
        isGhostActive = false;
       

    }
}



   









