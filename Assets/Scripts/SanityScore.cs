using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SanityScore : MonoBehaviour
{
    public float sanity, MaxSanity;

    public float decreaseSpeed = 1f;

    public bool sanityDecrease;

    
    
    public string sceneName;


    public FadingObject effects;
    public void Start()
    {
        
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

        float normalisedSanity = sanity / MaxSanity;
        effects.SetIntensity(normalisedSanity);

       

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

        if(sanity <= 0)
        {
            Time.timeScale = 0;
            StartCoroutine(WaitForFade());

        }


      
      
    }

    public IEnumerator WaitForFade()
    {
        Debug.Log("Waiting...");

        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene(sceneName);

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
