using UnityEngine;
using Unity.XR.CoreUtils;
using JetBrains.Annotations;

public class Laptop : MonoBehaviour
{

  
    
    public GazeRayCast gaze;

  

    public GameObject lighter;
    public Animator anim;
    public GameObject video;

    public void Start()
    {
        lighter.SetActive(false);
    }

    public void Play()
    {
        video.SetActive(true);
        anim.SetBool("PlayAnim", true);
        
    }
    public void Spawn()
    {
        lighter.SetActive(true);
    }

    public void EndAnim()
    {
        video.SetActive(false);
        anim.SetBool("PlayAnim", false);
       
        //lockedHead = false;
        
    }
}
