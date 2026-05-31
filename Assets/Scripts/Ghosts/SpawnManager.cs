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


    private bool hallwaySpawning = false;
    private bool windowSpawning = false;


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



    
    public void StartWindowSpawnTimer()
    {
        if (!windowSpawning)
        {
            windowSpawning = true;
            StartCoroutine(WindowSpawn());
           
        }
      
      
    }


    public IEnumerator WindowSpawn()
    {
     
        yield return new WaitForSeconds(windowSpawnDelay);
        windowSpawning = false;
        if (windowPathPicker != null)
        {
            windowPathPicker.PathChosen();
        }
        else
        {
        }



    }

    /// <summary>
    /// /////////////////////////////////////////
    /// </summary>
    public void StartHallwaySpawnTimer()
    {
        if (!hallwaySpawning)
        {
            hallwaySpawning = true;
            StartCoroutine(HallwaySpawn());
        }
     
    }

    public IEnumerator HallwaySpawn()
    {
        yield return new WaitForSeconds(hallwaySpawnDelay);
        hallwaySpawning = false;
        if (hallwayPathPicker != null)
            hallwayPathPicker.PathChosen();
    }


    


    public void SetSpawnDelay(float newDelay)
    {
        hallwaySpawnDelay = newDelay;
        windowSpawnDelay = newDelay + 3f;

    }
} 
   
