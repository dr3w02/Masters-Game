using UnityEngine;
using UnityEngine.UI;



public class MenuGaze : MonoBehaviour
{
    Ray ray;

    RaycastHit hit;

    public float maxDistance = 300;
   

  
    [SerializeField]
    private string UI = "UI";



    public int waittime;

    public bool select;

    private Button _currentButton;

    public bool alreadySelected;

    [Header("Radial Timer")]
    [SerializeField] private float indicatorTimer;
    [SerializeField] private float maxIndicatorTimer;

    [Header("UI Indicator")]
    [SerializeField] private Image radialIndicatorUI = null;


    [Header("Gaze")]
    public OVREyeGaze leftEye;
    public OVREyeGaze rightEye;
    [SerializeField] private Transform centerEyeAnchor;


    [Header("Negative moves the ray DOWN, positive moves it UP.")]
    [Range(-0.5f, 0.5f)]
    public float verticalOffset = -0.1f;

    public void Start()
    {
        //indicatorTimer = waittime;
       maxIndicatorTimer = waittime;


    }

    public void Update()
    {

        Vector3 combinedDirection = (leftEye.transform.forward + rightEye.transform.forward).normalized;
        Vector3 combinedPosition = centerEyeAnchor.position;

        ray = new Ray(combinedPosition, combinedDirection);

        Debug.DrawRay(combinedPosition, combinedDirection * maxDistance, Color.cyan);

        CheckForColliders();


        if (select == true)
        {

            indicatorTimer += Time.deltaTime;
            radialIndicatorUI.enabled = true;
            radialIndicatorUI.fillAmount = indicatorTimer / maxIndicatorTimer;


            if (indicatorTimer >= waittime)
            {
                radialIndicatorUI.enabled = false;

                //indicatorTimer = maxIndicatorTimer;
               // radialIndicatorUI.fillAmount = maxIndicatorTimer;

                if (_currentButton != null)
                {
                    _currentButton.onClick.Invoke();
                }

                indicatorTimer = 0;
                select = false;
                alreadySelected = true;

            }
        }

        else
        {
            indicatorTimer = 0;

            radialIndicatorUI.fillAmount = 0;
            radialIndicatorUI.enabled = false;
     
            

        }
    }
 
    public void CheckForColliders()
    {



        int layerMask = ~(1 << 2);

        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            var hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag(UI))
            {
                if (!alreadySelected)
                {
                    _currentButton = hitObj.GetComponent<Button>();
                    select = true;
                }
                return;
            }
        }

       
        select = false;
        _currentButton = null;
        alreadySelected = false;

    }
}
