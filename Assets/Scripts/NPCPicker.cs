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
        
        if (isGhostActive)
        {
           
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
    
        if (SpawnState != null)
        {
            SpawnState.sisterGhostActive = false;
            SpawnState.StartHallwaySpawnTimer();
        }


    }


    public void EndOfPath(bool deductSanity = true)
    {
        isGhostActive=false;
      
        if (chosen == null)
        {
           
            return;
        }
 

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
           
            StartCoroutine(WaitBeforeSister(2f));
        }
        else
        {
            if (SpawnState != null)
            {
                SpawnState.StartHallwaySpawnTimer();
            }
            else
            {
               
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
       
        yield return new WaitForSeconds(2f);
      
        EndOfPath();
       

        if (SpawnState != null)
            SpawnState.StartHallwaySpawnTimer();
     
        
    }
}



   









