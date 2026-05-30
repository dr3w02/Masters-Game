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
            chosen = null;
        }
    }
   

    public IEnumerator StartNPCPicker(PathPicker pathPicker)
    {
        Debug.Log("isGhostActive = " + isGhostActive);
        if (isGhostActive)
        {
            Debug.Log("NoSpawn1");
            yield break;
        }

        isGhostActive = true;

        yield return new WaitForSeconds(delayBeforePick);


        var hallwayGhosts = _npcStatsSource.hallwayGhosts;


        if (hallwayGhosts == null || hallwayGhosts.Count == 0)
        {
            isGhostActive = false;
            Debug.Log("NoSpawn");
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
             
                var validGhosts = hallwayGhosts.Where(g => !g.sisterGhost).ToList();
                if (validGhosts.Count > 0)
                {
                    temporaryChosen = validGhosts[Random.Range(0, validGhosts.Count)];
                }
                else
                {
                    Debug.Log("No valid ghosts available.");
                    Debug.Log("NoSpawn2");
                    isGhostActive = false;
                    yield break;
                }
            }

            lastPickedIndex = index;
            chosen = temporaryChosen;

  
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
       

      
    }

    IEnumerator WaitBeforeSister(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("NoSpawn3");
        if (sisterSpawnState != null)
        {
            sisterSpawnState.sisterGhostActive = false;
            sisterSpawnState.StartHallwaySpawnTimer();
        }


    }


    public void EndOfPath() //maybe this should be here 
    {
        Debug.Log("EndOfPath: HallwayGhostEnded");
        Debug.Log("EndOfPath:isGhostActive = " + isGhostActive);
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
       
       

    }
}



   









