using UnityEngine;

public class GazeRayCast : MonoBehaviour
{
    public SanityScore _sanity;


    Ray ray;

    RaycastHit hit;

    float maxDistance = 3000;
    public LayerMask layersToHit;


    

    public string DontLook = "Looking";

    public void Start()
    {
        _sanity = FindAnyObjectByType<SanityScore>();
    }

    public void Update()
    {
        ray = new Ray(transform.position, transform.forward );
        CheckForColliders();
    }

    public void CheckForColliders()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.cyan);

        if (_sanity == null)
        {
            
            return;
        }
    
        if (Physics.Raycast(ray, out hit, maxDistance, layersToHit))
        {

            var hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag(DontLook))
            {
                _sanity.DecreaseSanity();

            }
            else
            {
                return;
            }



            // if (hitObj.CompareTag(DoLook))
            //{
            //    _sanity.IncreaseSanity();
            //}
            Debug.Log("hit" + hit.collider.gameObject.name);
        }
    }
}
