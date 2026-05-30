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




    Vector3 eyeposL, eyeposR;
    private OVRPlugin.EyeGazesState _currentEyeGazesState;
    public Transform leftEyeObj;
    public Transform rightEyeObj;


    public Transform reticleCircle;
    public float fixedDistance = 2.0f;


    public void Start()
    {
        alreadySelected = false;
        //indicatorTimer = waittime;
        maxIndicatorTimer = waittime;

        //if (!OVRPermissionsRequester.IsPermissionGranted(OVRPermissionsRequester.Permission.EyeTracking))
        //{
        //    OVRPermissionsRequester.Request(new[] { OVRPermissionsRequester.Permission.EyeTracking });
        //}
        //else
        //{
        //    Debug.LogWarning("NO EYETRACKING!");
        //}

        OVRPlugin.StartEyeTracking();

        Eyes.SetActive(true);
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
            
        //if (OVRPlugin.GetEyeGazesState(OVRPlugin.Step.Render, -1, ref _currentEyeGazesState))
        //{
        //    OVRPlugin.EyeGazeState eyeGazeL = _currentEyeGazesState.EyeGazes[(int)OVRPlugin.Eye.Left];
        //    OVRPlugin.EyeGazeState eyeGazeR = _currentEyeGazesState.EyeGazes[(int)OVRPlugin.Eye.Right];

        //    if (eyeGazeR.IsValid && eyeGazeL.IsValid)
        //    {
        //        if (eyeGazeL.Confidence >= 0.5f && eyeGazeR.Confidence >= 0.5f)
        //        {
        //            OVRPose poseL = eyeGazeL.Pose.ToOVRPose();
        //            OVRPose poseR = eyeGazeR.Pose.ToOVRPose();

        //            if (leftEyeObj != null)
        //            {
        //                leftEyeObj.localRotation = poseL.orientation;
        //                leftEyeObj.localPosition = poseL.position;
        //            }

        //            if (rightEyeObj != null)
        //            {
        //                rightEyeObj.localRotation = poseR.orientation;
        //                rightEyeObj.localPosition = poseR.position;
        //            }
        //        }
        //        else
        //        {
        //            Debug.LogWarning("Low Tracking Confidence");
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Eye Gazes Not Valid");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("No Eye Tracking State Found from Plugin");
        //}

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
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out hit, 4000f))
        {
            var selection = hit.transform;
            if (selection.CompareTag("UI"))
            {
                if (_currentButton != selection.GetComponent<Button>())
                {

                    var selectionRenderer = selection.GetComponent<Renderer>();

                    // if (selectionRenderer != null)
                    // {
                    Debug.Log("hit something");
                    //selectionRenderer.material = highlightMaterial;

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
     

