using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public bool Open;
    public bool Close;
    public SanityScore sanityscript;
    public float decreaseSpeed = 1f; // lose one sanity every second 

    public Animator animator;
 
    

    public void Update()
    {
        sanity();
    }
    public void sanity()
    {
        if (Open)
        {
            sanityscript.sanityDecrease = true;
        }

        if (Close)
        {
            sanityscript.sanityDecrease = false;
        }

    }



}
