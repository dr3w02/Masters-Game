using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldTime;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    public PathPicker _pathPicker;


    public float spawnDelay;
    public bool spawning;

    public bool sisterGhostActive;

    public void Awake()
    {
       
        //_pathPicker = FindFirstObjectByType<PathPicker>();


    }
    public void Start()
    {
        spawnDelay = 15;

        StartSpawning();
    }

   
    public void StartSpawning()
    {
        if (!spawning)
        {
            StartCoroutine(SpawnRate());
            
        }
    }

    private IEnumerator SpawnRate()
    {
        
            yield return new WaitForSeconds(spawnDelay);
            _pathPicker.PathChosen();
            StartCoroutine(SpawnRate());
        
       
    }

    public void SetSpawnDelay(float newDelay)
    {
        spawnDelay = newDelay;
    }
} 
   
