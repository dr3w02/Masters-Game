using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

   
    public Transform spawnPoint;
    public GameObject xrOrigin;



    public void Respawn()
    {
        xrOrigin.transform.position = spawnPoint.position;
        xrOrigin.transform.rotation = spawnPoint.rotation;
    }

    public void Start()
    {
        xrOrigin.transform.position = spawnPoint.position;
        xrOrigin.transform.rotation = spawnPoint.rotation;
    }

}
