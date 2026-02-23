using System;
using System.Collections.Generic;
using UnityEngine;
using WorldTime;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    public WorldTime.WorldTime worldTime;
   

    public PathPicker _pathPicker;

    public bool intensity;

    public void Awake()
    {
       
        _pathPicker = FindFirstObjectByType<PathPicker>();


    }

} 
   
