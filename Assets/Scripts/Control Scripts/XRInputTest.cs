using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputTest : MonoBehaviour
{
    public InputAction testAction;
    public InputActionProperty testProperty;
    public InputActionReference testValue;
    public InputActionReference testPassthrough;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testValue.action.performed += TestVauleAction;
        testPassthrough.action.performed += TestPassthroughAction;
    }


    //removing the button
    private void OnDestroy()
    {
        testValue.action.performed -= TestVauleAction;
        testPassthrough.action.performed -= TestPassthroughAction;
    
    }

    //generating the buttons 
    private void TestVauleAction(InputAction.CallbackContext callback)
    {
        float value = callback.ReadValue<float>();
        print($"value:{value}");
    }
    private void TestPassthroughAction(InputAction.CallbackContext callback)
    {
        float value = callback.ReadValue<float>();
        print($"passthrough:{value}");
    }

}
