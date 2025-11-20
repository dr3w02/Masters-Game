using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPicker : MonoBehaviour
{
    public NPCSTATS npcStatsSource;

    [SerializeField]
    private bool picked;

    public float intensity;

    public List<npcStatistics> list = new List<npcStatistics>();

    private void PickHallwayGhost()
    {
        List<npcStatistics> list = npcStatsSource.hallwayGhosts;

        if(list.Count == 0)
        {
            Debug.Log("ERROR: No NPCS!");

            return;
        }


        int randomIndex = Random.Range(0, list.Count);
        npcStatistics chosen = list[randomIndex];

        GameObject prefab = chosen.ghostPrefab;

        GameObject clone = Instantiate(prefab, transform.position, Quaternion.identity);
        clone.SetActive(true);
        clone.AddComponent<WayPointMover>();


        list.RemoveAt(randomIndex);




    }

    public void Start()
    {

       StartCoroutine(PickNpc());
    }


    


    public IEnumerator PickNpc()
    {
       
      
            Debug.Log("NPCPicked");
            PickHallwayGhost();

            yield return new WaitForSeconds(intensity);



    }
}







