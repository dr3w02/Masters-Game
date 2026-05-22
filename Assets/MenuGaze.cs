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


    public void Start()
    {
        //indicatorTimer = waittime;
       maxIndicatorTimer = waittime;


    }

    public void Update()
    {
   

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
            //indicatorTimer -= Time.deltaTime;
            //    radialIndicatorUI.fillAmount = indicatorTimer;

            //    if (indicatorTimer <= maxIndicatorTimer)
            //    {
            //        indicatorTimer = maxIndicatorTimer;
            //        radialIndicatorUI.fillAmount = maxIndicatorTimer;
            //        radialIndicatorUI.enabled = false;
            //        select = false;
            //    }
            

        }
    }
 
    public void CheckForColliders()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.black);


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
