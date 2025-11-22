using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SanityScore : MonoBehaviour
{
    public float sanity, MaxSanity;

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
        sanity += sanityChange; 
        sanity = Mathf.Clamp(sanity, 0, MaxSanity);

        //Sanity += sanityChange; means I can change sanity though +20 -20 if it was sanity = amount would make +20 set sanity to 20 

    }

    public void SanityHealth()
    {
        //To Lower Sanity 
        //SetSanity(-20f);
        //To Add Sanity
        //SetSanity(+20f);
        if (sanity < 20)
        {
            AudioManager.instance.PlaySFX("Breathing");
        }
        else
        {
            AudioManager.instance.StopSFX("Breathing");
        }

        if (sanity > 20)
        {
           


        }


        if (sanity == 0)
        {
           //DIE!
        }

      
    }

    public void DecreaseSanity()
    {
        SetSanity(sanity -= decreaseSpeed * Time.deltaTime);
        Debug.Log("Current Sanity: " + sanity);
    }
}
