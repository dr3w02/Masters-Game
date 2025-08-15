using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private void Update()
    {
        
    }

    void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Selected");
        }
    }
}
