using UnityEngine;
using System.Collections;


public class GoBackToPosition : MonoBehaviour
{
    public Vector3 _startPosition;
    private Quaternion _startRotation;


    void Start()
    {
  
        _startPosition = transform.position;
        _startRotation = transform.rotation;

    }

    public void TeleportToStart()
    {

        StartCoroutine(LighterWait());
    }

  

    IEnumerator LighterWait()
    {
        yield return new WaitForSeconds(2.5f);

        transform.position = _startPosition;
        transform.rotation = _startRotation;

        StopCoroutine(LighterWait());


    }
}
