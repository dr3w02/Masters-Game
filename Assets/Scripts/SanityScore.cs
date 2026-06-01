using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SanityScore : MonoBehaviour
{
    public float sanity, MaxSanity;

    public float decreaseSpeed = 1f;

    public bool sanityDecrease;

    
    
 


    public FadingObject effects;
    public FadeScreen fade;


    public AudioSource imagePrompt;
    public bool prompt;
    public CandleManager Candle;

    public void Start()
    {
        
        Time.timeScale = 1;
        sanity = 100;
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
            if (!prompt)
            {
                imagePrompt.Play();
               
                prompt = true;
            }
            if (prompt)
            {
                imagePrompt.Pause();

            }

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
            prompt = false;
            NPCS.SetActive(false);
            Candle.mainScene = false;
            StartCoroutine(WaitForFade());

        }


      
      
    }
    public GameObject NPCS;

    public IEnumerator WaitForFade()
    {
       
        fade.FadeOut();

        yield return new WaitForSeconds(5f);
        NPCS.SetActive(true);
        SceneManager.LoadScene(4);
        

    }

    public void DecreaseSanity()
    {
        SetSanity(-decreaseSpeed * Time.deltaTime);
     
    }

    public void IncreaseSanity()
    {
        SetSanity(+decreaseSpeed * Time.deltaTime);
      
    }
  
}
