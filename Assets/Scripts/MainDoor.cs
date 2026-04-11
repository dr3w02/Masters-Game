using UnityEngine;

public class MainDoor : MonoBehaviour
{
    
    public bool open = true;
    public SanityScore _sanity;
    public float decreaseSpeed = 1f; // lose one sanity every second 

    public Animator anim;

    public NPCPicker _npcPicker;
    public void Awake()
    {
        anim.SetBool("Open", true);
        anim.SetBool("Close", false);
    }

    public void OpenDoor()
    {
        if (open)
        {

            anim.SetBool("Open", true);
            anim.SetBool("Close", false);
            open = false;
            _npcPicker.doorClosed = false;

  

        }
        else
        {

          //  _npcPicker.DoorClosed();

            anim.SetBool("Open", false);
            anim.SetBool("Close", true);
            open = true;
            // sanity();

            

        }



    }


    public void sanity()
    {
        if (open)
        {
            _sanity.DecreaseSanity();
        }

        else
        {
            return;

        }

    }



}
