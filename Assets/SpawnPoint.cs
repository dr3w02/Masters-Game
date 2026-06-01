using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

   
    public Transform spawnPoint;
    public GameObject xrOrigin;



    public void Respawn()
    {
        xrOrigin.transform.position = spawnPoint.transform.position;
        xrOrigin.transform.rotation = spawnPoint.transform.rotation;

    }

    public void Start()
    {
        if (xrOrigin == null) return;

        xrOrigin.transform.position = spawnPoint.position;
        xrOrigin.transform.rotation = spawnPoint.transform.rotation;


    }

}
