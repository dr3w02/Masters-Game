using UnityEngine;

public class GoBackToPosition : MonoBehaviour
{
    private Vector3 _startPosition;


    void Start()
    {
  
        _startPosition = transform.position;
    
    }

    public void TeleportToStart()
    {
        transform.position = _startPosition;
       
    }
}
