using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GazeRayCast : MonoBehaviour
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

    [SerializeField]
    private string UI = "UI";




    public int waittime;

    private bool select;

    private Button _currentButton;

    [Header("Radial Timer")]
    [SerializeField] private float indicatorTimer;
    [SerializeField] private float maxIndicatorTimer;

    [Header("UI Indicator")]
    [SerializeField] private Image radialIndicatorUI = null;

 

    


    public void Start()
    {
        _sanity = FindAnyObjectByType<SanityScore>();
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
          

            Debug.Log("hit" + hit.collider.gameObject.name);



        }
    }





}
