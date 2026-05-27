using UnityEngine;
using Unity.XR.CoreUtils;
using JetBrains.Annotations;

public class Laptop : MonoBehaviour
{

  
    
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
        
    }
}
