using UnityEngine;

public class UIRayCast : MonoBehaviour
{
    public SanityScore _sanity;


    Ray ray;

    RaycastHit hit;

    public float maxDistance = 300;
    public LayerMask layersToHit;

    [SerializeField]
    private string DontLook = "SanityDecrease";
    [SerializeField]
    private string DoLook = "SanityIncrease";

    public void Start()
    {
        _sanity = FindAnyObjectByType<SanityScore>();
    }

    public void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.cyan);

        CheckForColliders();
    }

    public void CheckForColliders()
    {


        if (Physics.Raycast(ray, out hit, maxDistance))
        {

            var hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag(DontLook))
            {
                _sanity.DecreaseSanity();
               

            }

            if (hitObj.CompareTag(DoLook))
            {
                _sanity.IncreaseSanity();
                
            }


         
        }
    }
}
