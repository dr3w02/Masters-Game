using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    private Animator animatorDoor;
    private bool isOpen = false;
    private bool isClosed = false;

    void Start()
    {
        animatorDoor = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {

        if (!isOpen)
        {
            animatorDoor.SetBool("isOpen", isOpen);
            animatorDoor.SetBool("isClosed", !isClosed);
            isOpen = true;
        }
        else
        {
            isOpen = false;
            animatorDoor.SetBool("isClosed", isClosed);
            animatorDoor.SetBool("isOpen", !isOpen);
        }
     
        
      
    }

   
       
    
}
