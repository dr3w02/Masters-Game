using UnityEngine;

public class FadeGhosts : MonoBehaviour
{
    public SkinnedMeshRenderer ghostRenderer;
    public float fadeSpeed = 0.5f;

    private float fadeValue = 0f;
    private bool shouldFade = false;

    [SerializeField] private NPCPicker _npcPicker;

    [SerializeField] private W_NPCPicker _windowNpcPicker;

    public bool sister;
    public bool window;

    public AudioSource ghostChuckle;

    public void TriggerFade()
    {
        Debug.Log("Fade Triggered");
        shouldFade = true;
    }

    void Update()
    {
        if (shouldFade)
        {
           
            fadeValue += Time.deltaTime * fadeSpeed;
            fadeValue = Mathf.Clamp(fadeValue, 0f, 5f);

            ghostRenderer.material.SetFloat("_Fade", fadeValue);


            //Delete

            if (fadeValue >= 5f)
            {
                ghostChuckle.Play();

                if (sister)
                {
                  
                    sister = false;
                    _windowNpcPicker.SisterEndOfPath();
                    Debug.Log("Sisterendofpath");
                }

                if (window)
                {
                    
                    window = false;
                    _windowNpcPicker.EndOfPath();
                    Debug.Log("Windowendofpath");
                }
                else
                {
                    
                    _npcPicker.EndOfPath();

                }


            }
        }
    }
}
