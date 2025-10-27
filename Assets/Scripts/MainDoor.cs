using UnityEngine;

public class MainDoor : MonoBehaviour
{
    
    public bool Closed;
    public SanityScore sanityscript;
    public float decreaseSpeed = 1f; // lose one sanity every second 

    public Animator anim;
 
   
    public void Update()
    {
        sanity();
    }

    public void OpenDoor()
    {
        if (!Closed)
        {
            anim.SetBool("Open", false);
            anim.SetBool("Close", true);
            Closed = true;
            
        }
        else
        {
            anim.SetBool("Open", true);
            anim.SetBool("Close", false);
            Closed = false;
        }
   
    }


    public void sanity()
    {
        if (!Closed)
        {
            sanityscript.sanityDecrease = false;
        }

        else
        {
            sanityscript.sanityDecrease = true;
        }

    }



}
