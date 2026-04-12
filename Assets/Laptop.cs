using UnityEngine;

public class Laptop : MonoBehaviour
{
    public Camera mainCam;
    public Transform camLockTrans;

    public Animator Anim;

    public void LockCamera()
    {
        mainCam.transform.position = camLockTrans.position;


    }
}
