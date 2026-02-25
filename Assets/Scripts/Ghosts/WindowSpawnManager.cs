using System.Collections;
using UnityEngine;

public class WindowSpawnManager : MonoBehaviour
{
    [Header("References")]
    public PathPicker _pathPicker;


    public float windowspawnDelay;
    public bool spawning;

    public void Awake()
    {

       


    }
    public void Start()
    {
        windowspawnDelay = 30;

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

        yield return new WaitForSeconds(windowspawnDelay);
        _pathPicker.PathChosen();
        StartCoroutine(SpawnRate());


    }

    public void SetSpawnDelay(float newDelay)
    {
        windowspawnDelay = newDelay;
    }
}
