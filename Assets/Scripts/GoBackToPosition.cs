using UnityEngine;

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
        transform.position = _startPosition;
        transform.rotation = _startRotation;

    }
}
