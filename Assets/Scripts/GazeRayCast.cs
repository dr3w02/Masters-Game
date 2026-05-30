using UnityEngine;



public class GazeRayCast : MonoBehaviour
{
    public SanityScore _sanity;
    public W_NPCPicker _wNpcPicker;
    public Laptop laptop;

    Ray gazeray;

    RaycastHit hit;

    public float maxDistance = 300;



    [SerializeField] private string DontLook = "SanityDecrease";
    [SerializeField] private string DoLook = "SanityIncrease";
    [SerializeField] private string WindowGhosts = "WindowGhosts";
    [SerializeField] private string Laptop = "Laptop";

    

    [Header("Laptop")]
    public bool lookingAtLaptop;

    Vector3 eyeposL, eyeposR;
    private OVRPlugin.EyeGazesState _currentEyeGazesState;
    public Transform leftEyeObj;
    public Transform rightEyeObj;

    public Transform gaze;


    public void Start()
    {
        OVRPlugin.StartEyeTracking();

        _sanity = FindAnyObjectByType<SanityScore>();
        _wNpcPicker = FindAnyObjectByType<W_NPCPicker>();

        if (!OVRPermissionsRequester.IsPermissionGranted(OVRPermissionsRequester.Permission.EyeTracking))
        {
            OVRPermissionsRequester.Request(new[] { OVRPermissionsRequester.Permission.EyeTracking });
        }

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


                    if (leftEyeObj != null)
                    {
                        leftEyeObj.localRotation = poseL.orientation;
                        leftEyeObj.localPosition = poseL.position;
                    }

                    if (rightEyeObj != null)
                    {
                        rightEyeObj.localRotation = poseR.orientation;
                        rightEyeObj.localPosition = poseR.position;
                    }

                    gaze.localRotation = poseR.orientation;
                    gaze.localPosition = poseR.position;
                }
                else
                {
                   
                }
            }
            else
            {
             
            }
        }
        else
        {
         
        }

        CheckForColliders();
    }

    public void CheckForColliders()
    {


        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            var hitObj = hit.collider.gameObject;
       

            if (hitObj.CompareTag(DontLook))
            {
                _sanity.DecreaseSanity();
              

            }

            if (hitObj.CompareTag(DoLook))
            {
                _sanity.IncreaseSanity();
            
            }



            if (hitObj.CompareTag(WindowGhosts))
            {
              
                _wNpcPicker.LookedAt();
               
            }


            if (hitObj.CompareTag(Laptop))
            {
               

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


     


        }

        
    }




 
}
