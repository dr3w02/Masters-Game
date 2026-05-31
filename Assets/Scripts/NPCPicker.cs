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
    public SpawnManager SpawnState;

    [Header("Spawn Control")]
    public bool isGhostActive = false;

    private int lastPickedIndex = -1;

    public bool prompted;
    private int promptedtimes;
    public AudioSource doorWarning;
    public void Awake()
    {
        if (_npcStatsSource == null) _npcStatsSource = GetComponent<NPCSTATS>();
        if (_sanityScore == null) _sanityScore = FindFirstObjectByType<SanityScore>();
        if (SpawnState == null) SpawnState = FindFirstObjectByType<SpawnManager>();
        if(doorWarning == null) doorWarning = GetComponent<AudioSource>();

    }
    private void Start()
    {
        promptedtimes = 0;
        prompted = false;

    }
     public void Update()
    {
        //if (_sanityScore.sanity <= 0 && chosen != null && chosen.ghostPrefab != null)
        //{
        //    chosen.ghostPrefab.SetActive(false);
        //    isGhostActive = false;
        //    chosen = null;
        //    if (SpawnState != null)
        //        SpawnState.StartHallwaySpawnTimer();
        //}

        if (_sanityScore.sanity <= 0)
        {
            chosen.ghostPrefab.SetActive(false);
            isGhostActive = false;
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
            npcStatistics temporaryChosen = hallwayGhosts[index];

            if (hallwayGhosts.Count > 1)
            {
                while (index == lastPickedIndex)
                {
                    index = Random.Range(0, hallwayGhosts.Count);
                }
            }


            if (temporaryChosen.sisterGhost && SpawnState.sisterGhostActive)
            {
             
                var validGhosts = hallwayGhosts.Where(g => !g.sisterGhost).ToList();
                if (validGhosts.Count > 0)
                {
                    temporaryChosen = validGhosts[Random.Range(0, validGhosts.Count)];
                    pathPicker.PathChosen();
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
                SpawnState.sisterGhostActive = true;
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
        if (SpawnState != null)
        {
            SpawnState.sisterGhostActive = false;
            SpawnState.StartHallwaySpawnTimer();
        }


    }


    public void EndOfPath(bool deductSanity = true)
    {
        isGhostActive=false;
        Debug.Log("EndOfPath: called");
        if (chosen == null)
        {
            Debug.Log("EndOfPath: chosen is null, bailing");
            return;
        }
        Debug.Log("EndOfPath: chosen = " + chosen);

        if (chosen.ghostPrefab != null)
        {
            chosen.ghostPrefab.SetActive(false);
        }

        if (deductSanity)
        {
            _sanityScore.sanity -= chosen.sanityAmount;

            if (!prompted)
            {
                doorWarning.Play();
                prompted = true;
            }
        }
           


        if (chosen.sisterGhost)
        {
            Debug.Log("EndOfPath: sister ghost, starting WaitBeforeSister");
            StartCoroutine(WaitBeforeSister(2f));
        }
        else
        {
            if (SpawnState != null)
            {
                Debug.Log("EndOfPath: starting timer");
                SpawnState.StartHallwaySpawnTimer();
            }
            else
            {
                Debug.Log("EndOfPath: SpawnState is null!");
            }

        }

        if (SpawnState != null)
        {
            SpawnState.StartWindowSpawnTimer();
        }



        chosen = null;

    }

  

    public IEnumerator WaitForFadeAndDespawn()
    {
        Debug.Log("WaitForFadeAndDespawn: started");
        yield return new WaitForSeconds(2f);
        Debug.Log("WaitForFadeAndDespawn: calling EndOfPath");
        EndOfPath();
       

        if (SpawnState != null)
            SpawnState.StartHallwaySpawnTimer();
        else
            Debug.Log("WaitForFadeAndDespawn: SpawnState still null!");
    }
}



   









