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


    public void Start()
    {
        //indicatorTimer = waittime;
       maxIndicatorTimer = waittime;


    }

    public void Update()
    {

        Vector3 combinedDirection = (leftEye.transform.forward + rightEye.transform.forward).normalized;
        Vector3 combinedPosition = (leftEye.transform.position + rightEye.transform.position) / 2f;

        ray = new Ray(combinedPosition, combinedDirection);
        Debug.DrawRay(combinedPosition, combinedDirection * maxDistance, Color.grey);

     
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
     


        if (Physics.Raycast(ray, out hit, maxDistance))
        {

            var hitObj = hit.collider.gameObject;

          

            if (hitObj.CompareTag(UI) && !alreadySelected) 
            {
                _currentButton = hitObj.GetComponent<Button>();
                select = true;

                Debug.Log("HitUI");
                return;


            }

            
            select = false;
            _currentButton = null;
            alreadySelected = false;

            Debug.Log("DontHitUI");




            Debug.Log("hit" + hit.collider.gameObject.name);


        }


    }
}
