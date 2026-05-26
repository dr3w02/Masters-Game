using UnityEngine;


public class GazeRayCast : MonoBehaviour
{
    public SanityScore _sanity;
    public W_NPCPicker _wNpcPicker;
    public Laptop laptop;

    Ray ray;

    RaycastHit hit;

    public float maxDistance = 300;


    [SerializeField] private string DontLook = "SanityDecrease";
    [SerializeField] private string DoLook = "SanityIncrease";
    [SerializeField] private string WindowGhosts = "WindowGhosts";
    [SerializeField] private string Laptop = "Laptop";

    
    


    [Header("Laptop")]
    public bool lookingAtLaptop;

    public OVREyeGaze leftEye;
    public OVREyeGaze rightEye;

    public void Start()
    {
        _sanity = FindAnyObjectByType<SanityScore>();
        _wNpcPicker = FindAnyObjectByType<W_NPCPicker>();
    
    }

    public void Update()
    {
        Vector3 combinedDirection = (leftEye.transform.forward + rightEye.transform.forward).normalized;
        Vector3 combinedPosition = (leftEye.transform.position + rightEye.transform.position) / 2f;

        ray = new Ray(combinedPosition, combinedDirection);
        Debug.DrawRay(combinedPosition, combinedDirection * maxDistance, Color.cyan);

        CheckForColliders();

    }

    public void CheckForColliders()
    {
        if (!Physics.Raycast(ray, out hit, maxDistance)) return;

        var hitObj = hit.collider.gameObject;


        if (Physics.Raycast(ray, out hit, maxDistance))///////////////////////////////////////////
        {

            if (hitObj.CompareTag(DontLook))
            {
                _sanity.DecreaseSanity();
                Debug.Log("hit D" + hit.collider.gameObject.name);

            }

            if (hitObj.CompareTag(DoLook))
            {
                _sanity.IncreaseSanity();
                Debug.Log("hit I" + hit.collider.gameObject.name);
            }



            if (hitObj.CompareTag(WindowGhosts))
            {
                Debug.Log("Gaze dismissed window ghost: " + hitObj.name);
                _wNpcPicker.LookedAt();
               
            }


            if (hitObj.CompareTag(Laptop))
            {
                Debug.Log("Look at Laptop!");

                if (!lookingAtLaptop)
                {
                    laptop.Play();
                    lookingAtLaptop = true;
                }
               
                
           

            }
            
            else
            {
                //laptop.Pause();
            }


            Debug.Log("hit" + hit.collider.gameObject.name);


        }

        
    }




 
}
