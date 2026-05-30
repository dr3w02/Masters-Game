using UnityEngine;
using UnityEngine.UI;



public class MenuGaze : MonoBehaviour
{
    Ray gazeray;

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

    public GameObject Eyes;



    public Transform leftEyeObj;
    public Transform rightEyeObj;


    public Transform reticleCircle;
    public float fixedDistance = 2.0f;


    public void Start()
    {
        alreadySelected = false;
      
        maxIndicatorTimer = waittime;


        OVRPlugin.StartEyeTracking();

        Eyes.SetActive(true);
    }

    public void Update()
    {
        

            if (OVRPlugin.eyeTrackingEnabled)
            {

            }
            else
            {

                OVRPlugin.StartEyeTracking();
            }


            CheckForColliders();

            if (select == true)
            {

                indicatorTimer += Time.deltaTime;
                radialIndicatorUI.enabled = true;
                radialIndicatorUI.fillAmount = indicatorTimer / maxIndicatorTimer;


                if (indicatorTimer >= waittime)
                {
                    radialIndicatorUI.enabled = false;


                    if (_currentButton != null)
                    {
                        Debug.LogWarning("Invoke" + gameObject.name);
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

            Debug.LogWarning($"tracking:{OVRPlugin.eyeTrackingEnabled} ,  supported:{OVRPlugin.eyeTrackingSupported}");



            if (reticleCircle != null)
            {
                Ray ray = new Ray(transform.position, transform.forward);

                reticleCircle.position = ray.origin + (ray.direction * fixedDistance);

                reticleCircle.rotation = transform.rotation;
            }



        
    }




        public void CheckForColliders()
        {

            RaycastHit hit;

           

            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, maxDistance);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
            Debug.Log("hit something" + hit);
            var selection = hit.transform;
                if (selection.CompareTag(UI))
                {
                    if (_currentButton != selection.GetComponent<Button>())
                    {

                        var selectionRenderer = selection.GetComponent<Renderer>();

                       
                        Debug.Log("hit something");
                       

                        _currentButton = selection.GetComponent<Button>();
                        select = true;
                        indicatorTimer = 0;
                    }
                }
                else
                {
                    select = false;
                    _currentButton = null;
                }
            }
            else
            {
                select = false;
                _currentButton = null;
            }


        }
    
}
     

