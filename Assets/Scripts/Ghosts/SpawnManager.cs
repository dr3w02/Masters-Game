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
 


    public void Awake()
    {
       
        


    }
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
        Debug.Log("New hallway");
        StartCoroutine(HallwaySpawn());
    }

    
    public void StartWindowSpawnTimer()
    {
        StartCoroutine(WindowSpawn());
    }

    public IEnumerator HallwaySpawn()
    {
        
        yield return new WaitForSeconds(hallwaySpawnDelay);


        Debug.Log("Spawn");

        if (hallwayPathPicker != null)
        {
            hallwayPathPicker.PathChosen();
        }
        else
        {
            Debug.Log("CannotPickHallway");
        }

        
       
    }
    public IEnumerator WindowSpawn()
    {

        yield return new WaitForSeconds(windowSpawnDelay);

        if (hallwayPathPicker != null)
        {
            windowPathPicker.PathChosen();
        }
        else
        {
            Debug.Log("CannotPickWindow");
        }



    }


    public void SetSpawnDelay(float newDelay)
    {
        hallwaySpawnDelay = newDelay;
        windowSpawnDelay = newDelay + 3f;
    }
} 
   
