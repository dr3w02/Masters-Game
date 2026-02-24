using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    public Animator animatorDoor;
    public bool doorOpen = true;
  

    void Start()
    {
        
    }

    public void ToggleDoor()
    {
        Debug.Log("Door Activated");

        if (doorOpen == false)
        {

            animatorDoor.SetBool("isOpen", true);
            animatorDoor.SetBool("isClosed", false);
            doorOpen = true;
        }
        if (doorOpen == true)
        {
            doorOpen = false;
            animatorDoor.SetBool("isOpen", false);
            animatorDoor.SetBool("isClosed", true);
        }

    }
        
      
    

   
       
    
}
