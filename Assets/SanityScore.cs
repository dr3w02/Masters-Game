using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SanityScore : MonoBehaviour
{
    public float Sanity, MaxSanity;

    public float decreaseSpeed = 1f;

    public bool sanityDecrease;
  

    public void setMaxSanity(float maxSanity)
    {
        MaxSanity = maxSanity;
    }

    public void Update()
    {
        SanityHealth();

        if (sanityDecrease)
        {
            DecreaseSanity();
        }
        else
        {
           
        }

    }
    public void SetSanity( float sanityChange)
    {
        Sanity += sanityChange; 
        Sanity = Mathf.Clamp(Sanity, 0, MaxSanity);

        //Sanity += sanityChange; means I can change sanity though +20 -20 if it was sanity = amount would make +20 set sanity to 20 

    }

    public void SanityHealth()
    {
        //To Lower Sanity 
        //SetSanity(-20f);
        //To Add Sanity
        //SetSanity(+20f);
        if (Sanity < 20)
        {
            AudioManager.instance.PlaySFX("Breathing");
        }

        if (Sanity > 20)
        {
           


        }


        if (Sanity == 0)
        {
           //DIE!
        }

      
    }

    public void DecreaseSanity()
    {
        SetSanity(Sanity -= decreaseSpeed * Time.deltaTime);
        Debug.Log("Current Sanity: " + Sanity);
    }
}
