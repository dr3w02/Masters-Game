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

    [Header("Audio")]
    public AudioSource windowWarning;
    public bool prompted;
    private int promptedtimes;

    [Header("SisterSpawned")]
    public SpawnManager sisterSpawnState;
   

    public void Awake()
    {
        _npcStatsSource = GetComponent<NPCSTATS>();
        _sanityScore = FindFirstObjectByType<SanityScore>();
        windowWarning = GetComponent<AudioSource>();

        sisterSpawnState = FindFirstObjectByType<SpawnManager>();

    }
    private void Start()
    {
        promptedtimes = 0;
        prompted = false;

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
            npcStatistics temporaryChosen = windowGhosts[index];
          

            if (sisterSpawnState.sisterGhostActive)
            {
                pathPicker.PathChosen();
                Debug.Log("URDONE");

                yield break;
            }

            chosen = temporaryChosen;
            Debug.Log("Picked WINDOW ghost: " + chosen.ghostType);

            if (chosen.sisterGhost)
            {
                sisterSpawnState.sisterGhostActive = true;
            }



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

        if (chosen.ghostPrefab != null)
        {
            chosen.ghostPrefab.SetActive(false);
        }

     
        Debug.Log("End Of Path");
        _sanityScore.sanity -= 5;

        promptedtimes += 1;

        if (!prompted)
        {
            windowWarning.Play();
            prompted = true;
        }

        if(promptedtimes == 2)
        {
            prompted = false;
        }

        if (chosen.sisterGhost)
        {
            sisterSpawnState.sisterGhostActive = false;
        }

        if (sisterSpawnState != null)
        {
            sisterSpawnState.StartWindowSpawnTimer();
        }




    }
    public void SisterEndOfPath()
    {

        if (chosen == null)
        {
            return;
        }
        if (chosen.ghostPrefab != null)
        {
            chosen.ghostPrefab.SetActive(false);
        }

        Debug.Log("End Of Path");
        _sanityScore.sanity += 5;

        if (chosen.sisterGhost)
        {
            sisterSpawnState.sisterGhostActive = false;
        }

        if (sisterSpawnState != null)
        {
            sisterSpawnState.StartWindowSpawnTimer();
        }



    }

    public void LookedAt()
    {
        if (chosen == null)
        {
            return;
        }

        chosen.ghostPrefab.SetActive(false);

        if (chosen.sisterGhost)
        {
            sisterSpawnState.sisterGhostActive = false;
        }

        chosen = null;
    }

}
