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
    public LayerMask layersToHit;

    [SerializeField]
    private string DontLook = "SanityDecrease";
    [SerializeField]
    private string DoLook = "SanityIncrease";

    [SerializeField]
    private string UI = "UI";

    [SerializeField]
    private string WindowGhosts = "WindowGhosts";

    [SerializeField]
    private string Laptop = "Laptop";


    public int waittime;

    private bool select;

    private Button _currentButton;

    [Header("Radial Timer")]
    [SerializeField] private float indicatorTimer;
    [SerializeField] private float maxIndicatorTimer;

    [Header("UI Indicator")]
    [SerializeField] private Image radialIndicatorUI = null;


    [Header("StartLaptop")]
    public bool lookingAtLaptop;






    public void Start()
    {
        _sanity = FindAnyObjectByType<SanityScore>();
        _wNpcPicker = FindAnyObjectByType<W_NPCPicker>();
        indicatorTimer = waittime;
        maxIndicatorTimer = waittime;


    }

    public void Update()
    {
        ray = new Ray(transform.position, transform.forward );
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.cyan);

        CheckForColliders();

        if (select == true)
        {
            
            indicatorTimer -= Time.deltaTime;
            radialIndicatorUI.enabled = true;
            radialIndicatorUI.fillAmount = indicatorTimer;


            if (indicatorTimer <= 0)
            {
                indicatorTimer = maxIndicatorTimer;
                radialIndicatorUI.fillAmount = maxIndicatorTimer;

                radialIndicatorUI.enabled = false;
                if (_currentButton != null)
                {
                    _currentButton.onClick.Invoke();
                }
                    
                select = false;
            }
        }

        else
        { 
            if(!select)
            {
                indicatorTimer += Time.deltaTime;
                radialIndicatorUI.fillAmount = indicatorTimer;

                if(indicatorTimer >= maxIndicatorTimer)
                {
                    indicatorTimer = maxIndicatorTimer;
                    radialIndicatorUI.fillAmount = maxIndicatorTimer;
                    radialIndicatorUI.enabled = false;
                    select = false;
                }
            }

        }
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

            if (hitObj.CompareTag(UI))
            {
                _currentButton = hitObj.GetComponent<Button>();
                select = true;
                Debug.Log("HitUI");
            }
            else
            {
                select = false;
                _currentButton = null;
                indicatorTimer = maxIndicatorTimer;
                radialIndicatorUI.fillAmount = maxIndicatorTimer;
                radialIndicatorUI.enabled = false;

                Debug.Log("HitUI");
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
                    laptop.LockCamera();
                    lookingAtLaptop = true;
                }
                else
                {
                    laptop.Play();
                }
           

            }
            
            else
            {
                laptop.Pause();
            }

            Debug.Log("hit" + hit.collider.gameObject.name);


        }

        else
        {
            // only care about cancelling UI here
            select = false;
            _currentButton = null;
            indicatorTimer = maxIndicatorTimer;
            radialIndicatorUI.fillAmount = maxIndicatorTimer;
            radialIndicatorUI.enabled = false;
        }
    }




 
}
