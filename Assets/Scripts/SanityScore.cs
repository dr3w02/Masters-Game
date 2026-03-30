using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SanityScore : MonoBehaviour
{
    public float sanity, MaxSanity;

    public float decreaseSpeed = 1f;

    public bool sanityDecrease;

    
    public GameObject gameOver;

    public EffectIntensity effects;
    public void Start()
    {
        

        gameOver.SetActive(false);
        Time.timeScale = 1;
    }
    public void setMaxSanity(float maxSanity)
    {
        MaxSanity = maxSanity;
    }

    public void Update()
    {


        if (sanityDecrease)
        {
            DecreaseSanity();
        }

        SanityHealth();

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


        if(sanity < 50)
        {
            float normalisedSanity = sanity / MaxSanity;
            float effectWeight = 1f - normalisedSanity;
            effects.SetIntensity(effectWeight);
        }
        else
        {
            float effectWeight = 0f;
            effects.SetIntensity(effectWeight);
        }
       


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

        if(sanity == 0)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }


      
      
    }

    public void DecreaseSanity()
    {
        SetSanity(-decreaseSpeed * Time.deltaTime);
        Debug.Log("Current Sanityy: " + sanity);
    }

    public void IncreaseSanity()
    {
        SetSanity(+decreaseSpeed * Time.deltaTime);
        Debug.Log("Current Sanity: " + sanity);
    }
  
}
