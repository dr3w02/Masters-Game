using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldTime;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    public PathPicker hallwayPathPicker;
    public PathPicker windowPathPicker;


    [Header("Spawn Settings")]
    public float hallwaySpawnDelay = 15f;
    public float windowSpawnDelay = 20f;


    private bool spawning = false;

    [Header("Sister States")]
    public bool sisterGhostActive;
 
    public void Start()
    {
        StartSpawning();
    }

   
    public void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(HallwaySpawn());
            StartCoroutine(WindowSpawn());

        }
    }


    public void StartHallwaySpawnTimer()
    {
    
        StartCoroutine(HallwaySpawn());
    }

    
    public void StartWindowSpawnTimer()
    {
      
        StartCoroutine(WindowSpawn());
    }

    public IEnumerator HallwaySpawn()
    {
        Debug.Log("NoSpawn6");
        yield return new WaitForSeconds(hallwaySpawnDelay);


        if (hallwayPathPicker != null)
        {
            hallwayPathPicker.PathChosen();
        }
        else
        {
            
        }

        
       
    }
    public IEnumerator WindowSpawn()
    {
        Debug.Log("NoSpawn7");
        yield return new WaitForSeconds(windowSpawnDelay);

        if (windowPathPicker != null)
        {
            windowPathPicker.PathChosen();
        }
        else
        {
        }



    }


    public void SetSpawnDelay(float newDelay)
    {
        hallwaySpawnDelay = newDelay;
        windowSpawnDelay = newDelay + 3f;

    }
} 
   
