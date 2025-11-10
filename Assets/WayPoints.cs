using Unity.VisualScripting;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [Range(0f,2f)]
    [SerializeField]
    private float waypointSize = 1f;

    [SerializeField]
    private NPCPicker npcPicker;

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
        }
    }
}
