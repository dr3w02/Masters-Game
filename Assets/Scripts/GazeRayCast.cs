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
    public Transform headTransform;


    public void Start()
    {
        _sanity = FindAnyObjectByType<SanityScore>();
        _wNpcPicker = FindAnyObjectByType<W_NPCPicker>();
    
    }

    public void Update()
    {

        CheckForColliders();

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
                    eyeposL = headTransform.TransformPoint(poseL.position);
                    eyeposR = headTransform.TransformPoint(poseR.position);
                    rightEyeObj.position = eyeposR;
                    rightEyeObj.rotation = poseR.orientation;
                    leftEyeObj.position = eyeposL;
                    leftEyeObj.rotation = poseL.orientation;
                    rightEyeObj.forward = headTransform.TransformDirection(rightEyeObj.forward);
                    leftEyeObj.forward = headTransform.TransformDirection(leftEyeObj.forward);
                }
            }
        }
        else
        {
            Debug.LogWarning("Not Valid");
        }
    }
      

    public void CheckForColliders()
    {

        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.magenta);

        if (Physics.Raycast(ray, out hit, 4000f))
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
                Debug.Log("Gaze dismissed window ghost: " + hitObj.name);
                _wNpcPicker.LookedAt();
               
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
