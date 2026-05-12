using UnityEngine;
using Unity.XR.CoreUtils;
using JetBrains.Annotations;

public class Laptop : MonoBehaviour
{

    [Header("Locking the camera to point")]
    public XROrigin xrOrigin;
    public Transform camLockTrans;
    public GazeRayCast gaze;

    private bool lockedHead;

    public GameObject lighter;
    public Animator anim;

    public void Start()
    {
        lighter.SetActive(false);
    }


    public void Update()
    {
        //if (lockedHead)
        //{
        //    Vector3 headOffsetChange = xrOrigin.Camera.transform.position - xrOrigin.transform.position;
        //    xrOrigin.transform.position = camLockTrans.position - headOffsetChange;
        //    Debug.Log("Locked!!!");
        //}
    }
    public void LockCamera()
    {
        //lockedHead = true;
        anim.speed = 1;

       

        anim.SetBool("PlayAnim", true);

    }

    public void Pause()
    {
        anim.speed = 0;

    }

    public void Play()
    {
        anim.speed = 1;
    }
    public void EndAnim()
    {
        anim.SetBool("PlayAnim", false);
        lighter.SetActive(true);
        //lockedHead = false;
        gaze.lookingAtLaptop = false;
    }
}
