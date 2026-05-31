using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerKey : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    
 
    private Vector3 playerVelocity;
    private bool grounded;
    public float gravity = -9.8f;

    public Camera cam;
    private Vector2 lookPos;
   
    private float xRotation = 0f;
    public float xSens = 30f;
    public float ySens = 30f;

    public void OnLook(InputAction.CallbackContext context)
    {
        lookPos = context.ReadValue<Vector2>();
    }
    public void OnClick(InputAction.CallbackContext context)
    {
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //Cursor.lockState = CursorLockMode.Locked;

     
        
       
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;
        playerLook();
    }

   public void playerLook()
    {
        xRotation = (lookPos.y * Time.deltaTime) * ySens;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (lookPos.x * Time.deltaTime)* xSens);


    }

   
}
