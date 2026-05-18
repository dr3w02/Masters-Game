using System.Collections;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    public Animator animatorDoor;
    public SanityScore _sanity;
    public NPCPicker _npcPicker;
    public bool doorOpen = true;

    public float doorClosedAllowed;

    public NPCSTATS _npcStatsSource;

    [Header("Audio")]
    public AudioSource doorWarning;
    

    public void Start()
    {
        doorWarning = FindAnyObjectByType<AudioSource>();

        if (_sanity == null)
        {
            Debug.LogError("no sanity script");
        }

        if (animatorDoor == null)
            Debug.LogError("animatorDoor is null!");

    }
    public void ToggleDoor()
    {
        Debug.Log("Door Activated");

    

        if (!doorOpen)
        {
            
            AudioManager.instance.PlaySFX("DoorCreak");
           
            animatorDoor.SetBool("isOpen", true);
            animatorDoor.SetBool("isClosed", false);
            doorOpen = true;
            Debug.Log("DoorOpen");
            Debug.Log("DoorClosed");
           

        }
        else
        {
           
            AudioManager.instance.PlaySFX("DoorCreak");
            animatorDoor.SetBool("isOpen", false);
            animatorDoor.SetBool("isClosed", true);
            doorOpen = false;

            StartCoroutine(SpawnRate());



        }

    }

    private IEnumerator SpawnRate()
    {

        AudioManager.instance.PlaySFX("DoorBang");


        yield return new WaitForSeconds(doorClosedAllowed);

        var hallwayGhosts = _npcStatsSource.hallwayGhosts;

        foreach (var ghost in hallwayGhosts)
        {
            if (ghost.ghostPrefab != null)
            {
                ghost.ghostPrefab.SetActive(false);
            }
        }

        AudioManager.instance.StopSFX("DoorBang");

        doorWarning.Play();




        if (doorOpen == false)
        {
            _sanity.sanityDecrease = true;
            StartCoroutine(SpawnRate());
            
        }
        else
        {
           
            _sanity.sanityDecrease = false;

            StopCoroutine(SpawnRate());
        }
        

    }

    public void SetDoorClosed(float newDelay)
    {
        doorClosedAllowed = newDelay;
    }



   
    

}
