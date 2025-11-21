using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPicker : MonoBehaviour
{
    public NPCSTATS npcStatsSource;

    
    public bool picked;

    public float intensity;

   

    public npcStatistics chosen;
    public int randomIndex;

    public bool isPickingActive = true;

    public void Awake()
    {
        npcStatsSource = FindFirstObjectByType<NPCSTATS>();
    }
    public void Start()
    {

      

      
    }
    public bool Once = false;
    public void FixedUpdate()
    {
        var ghosts = npcStatsSource.hallwayGhosts;
        if (ghosts.Count != 0 && Once == false)
        {
            PickHallwayGhost();
            Once = true;
        }
    }



    private void PickHallwayGhost()
    {
        
            var ghosts = npcStatsSource.hallwayGhosts;



            if (ghosts.Count == 0)
            {
                Debug.LogError("ERROR: No NPCS!");
                isPickingActive = false;
                return;
            }
            else
            {
                for (int i = 0; i < ghosts.Count; i++)
                {

                    Debug.Log("List Transfer" + npcStatsSource.hallwayGhosts);
                    int randomIndex = Random.Range(0, ghosts.Count);
                    npcStatistics chosen = ghosts[randomIndex];
                    GameObject chosenPrefab = chosen.ghostPrefab;
                    Debug.Log("NPCPicked");
                    GameObject clone = Instantiate(chosenPrefab, transform.position, Quaternion.identity);
                    WayPoints waypoints = clone.GetComponent<WayPoints>();
                    clone.SetActive(true);
                    clone.AddComponent<WayPoints>();


                    ghosts.RemoveAt(randomIndex);

                    picked = true;

                    if (waypoints == null || clone == null)
                    {
                        Debug.Log("waypoints and or clone missing");
                    }

                }
               

            



        }



    }

    public void Update()
    {
       // StartCoroutine(PickNpc());

    }
    public IEnumerator PickNpc()
    {

        while (isPickingActive)
        {

            PickHallwayGhost();

            yield return new WaitForSeconds(intensity);
        }



    }
}








