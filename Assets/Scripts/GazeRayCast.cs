using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

public class GazeRayCast : MonoBehaviour
{
    public SanityScore _sanity;
    public W_NPCPicker _wNpcPicker;
    public Laptop laptop;

    Ray ray;

    RaycastHit hit;

    public float maxDistance = 300;
    

    [SerializeField]
    private string DontLook = "SanityDecrease";
    [SerializeField]
    private string DoLook = "SanityIncrease";

    [SerializeField]
    private string WindowGhosts = "WindowGhosts";

    [SerializeField]
    private string Laptop = "Laptop";





    [Header("StartLaptop")]
    public bool lookingAtLaptop;



    public void Start()
    {
        _sanity = FindAnyObjectByType<SanityScore>();
        _wNpcPicker = FindAnyObjectByType<W_NPCPicker>();
    
    }

    public void Update()
    {
        ray = new Ray(transform.position, transform.forward );
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
                Debug.Log("hit D" + hit.collider.gameObject.name);

            }

            if (hitObj.CompareTag(DoLook))
            {
                _sanity.IncreaseSanity();
                Debug.Log("hit I" + hit.collider.gameObject.name);
            }

           

            if (hitObj.CompareTag(WindowGhosts))
            {
                _wNpcPicker.LookedAt();
                Debug.Log("HitWindowGhosts");
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
