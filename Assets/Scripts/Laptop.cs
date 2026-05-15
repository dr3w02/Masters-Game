using UnityEngine;
using Unity.XR.CoreUtils;
using JetBrains.Annotations;

public class Laptop : MonoBehaviour
{

    [Header("Locking the camera to point")]
    public XROrigin xrOrigin;
    
    public GazeRayCast gaze;

  

    public GameObject lighter;
    public Animator anim;

    public void Start()
    {
        lighter.SetActive(false);
    }

    public void Play()
    {
        anim.SetBool("PlayAnim", true);
        
    }

    public void EndAnim()
    {
        anim.SetBool("PlayAnim", false);
        lighter.SetActive(true);
        //lockedHead = false;
        gaze.lookingAtLaptop = false;
    }
}
