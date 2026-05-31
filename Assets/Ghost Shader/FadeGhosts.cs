using UnityEngine;

public class FadeGhosts : MonoBehaviour
{
    public SkinnedMeshRenderer ghostRenderer;
    public float fadeSpeed = 2f;

    private float fadeValue = 0f;
    private bool shouldFade = false;

    [SerializeField] private NPCPicker _npcPicker;

    [SerializeField] private W_NPCPicker _windowNpcPicker;

    public bool sister;
    public bool window;

  
    public void TriggerFade()
    {
      
      
        shouldFade = true;
    }

    void Update()
    {
        if (shouldFade)
        {
            if (!shouldFade) return;

            fadeValue += Time.deltaTime * fadeSpeed;
            fadeValue = Mathf.Clamp(fadeValue, 0f, 5f);

            ghostRenderer.material.SetFloat("_Fade", fadeValue);


            //Delete

            if (fadeValue >= 5f)
            {
               

                if (sister)
                {
                  
                    sister = false;
                    _windowNpcPicker.SisterEndOfPath();
                   
                }

                else if (window)
                {
                    
                    window = false;
                    _windowNpcPicker.EndOfPath();
                    
                
                }
                else
                {

                    _npcPicker.EndOfPath();


                }

                fadeValue = 0f;

             
                ghostRenderer.material.SetFloat("_Fade", fadeValue);


                shouldFade = false;

            }
        }
    }
}
