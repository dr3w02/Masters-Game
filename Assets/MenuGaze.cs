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


    //[Header("Gaze")]
    //public OVREyeGaze leftEye;
    //public OVREyeGaze rightEye;
    //[SerializeField] private Transform centerEyeAnchor;


    
    Vector3 eyeposL, eyeposR;
    private OVRPlugin.EyeGazesState _currentEyeGazesState;
    public Transform leftEyeObj;
    public Transform rightEyeObj;
    Transform headTransform;
    private int layerMask;

    public void Start()
    {
        //indicatorTimer = waittime;
       maxIndicatorTimer = waittime;


    }

    public void Update()
    {
        if (OVRPlugin.GetEyeGazesState(OVRPlugin.Step.Render, -1, ref _currentEyeGazesState))
        {
            OVRPlugin.EyeGazeState eyeGazeL = _currentEyeGazesState.EyeGazes[(int)OVRPlugin.Eye.Left];
            OVRPlugin.EyeGazeState eyeGazeR = _currentEyeGazesState.EyeGazes[(int)OVRPlugin.Eye.Right];

            if (eyeGazeR.IsValid && eyeGazeL.IsValid)
            {
                if (eyeGazeL.Confidence >= 0.5f && eyeGazeR.Confidence >= 0.5f)
                {
                    OVRPose poseL = eyeGazeL.Pose.ToOVRPose();
                    OVRPose poseR = eyeGazeR.Pose.ToOVRPose();
                    eyeposL = poseL.position;
                    eyeposR = poseR.position;
                    rightEyeObj.position = eyeposR;
                    rightEyeObj.rotation = poseR.orientation;
                    leftEyeObj.position = eyeposL;
                    leftEyeObj.rotation = poseL.orientation;
                    rightEyeObj.forward = rightEyeObj.forward;
                    leftEyeObj.forward = leftEyeObj.forward;
                }
            }
        }

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
        Debug.DrawRay(gazeray.origin, gazeray.direction * maxDistance, Color.black);
        gazeray.origin = eyeposL + 0.5f * (eyeposR - eyeposL);
        gazeray.direction = 0.5f * (leftEyeObj.forward + rightEyeObj.forward);
        if (Physics.Raycast(gazeray, out hit, 100f, layerMask))
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
     

