using Unity.VisualScripting;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [SerializeField]
    private NPCSTATS _npcStatsSource;
    [SerializeField]
    private NPCPicker _picked;


    [Range(0f,2f)]
    [SerializeField]
    private float waypointSize = 1f;

    public Transform currentWaypoint;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float distanceThreshold = 0.1f;


    private void Start()
    {


        _npcStatsSource = FindFirstObjectByType<NPCSTATS>();

        _picked = FindFirstObjectByType<NPCPicker>();


        int moveSpeed = _picked.chosen.ghostSpeed;

    }
    public void Update()
    {
        Movement();
    }

    private void OnDrawGizmos()
    {
       
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypointSize);

        }


        Gizmos.color = Color.red;

        for(int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i +1).position);
        }

        
    }

 
    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if(currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }

        if (currentWaypoint.GetSiblingIndex() >= transform.childCount - 1)
        {
            NPCPicker npcPicker = GetComponent<NPCPicker>();
            Destroy(npcPicker);
            Debug.Log("Reached the last waypoint!");

            return null;
        }


        else
        {
            return transform.GetChild(0); // Change this to if in box collider then disapear and lose points

            //if door closed = true turn off the game object after randdomized time 

            // made it to the last checkpoint decrease sanity by 5 
        }
    }


    private void Movement()
    {
        if (currentWaypoint == null)
        {
            Debug.LogWarning("Nothing here :(");
            return;
        }


        this.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // if the emeny is close to the way point it goes to the next one 
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            Transform next = GetNextWaypoint(currentWaypoint);


            if (next == null)
            {
                Destroy(gameObject);

                //make path free again
            }

            currentWaypoint = next;
            transform.LookAt(currentWaypoint);
        }

        else
        {
            Debug.Log("Last Checkpoint Reached");
            return;
        }
    }





}


