using UnityEngine;

public class FadeGhosts : MonoBehaviour
{
    // Drag your ghost's Mesh object here in the Inspector
    public Renderer ghostRenderer;
    public float fadeSpeed = 0.5f;

    private float fadeValue = 1f;
    private bool shouldFade = false;

    // Call this public function from your waypoint script when the ghost arrives
    public void TriggerFade()
    {
        shouldFade = true;
    }

    void Update()
    {
        if (shouldFade)
        {
            // Drop the value down over time
            fadeValue -= Time.deltaTime * fadeSpeed;

            // Send that number to your shader property
            ghostRenderer.material.SetFloat("_Fade", fadeValue);

            // Once it completely vanishes, delete the ghost
            if (fadeValue <= 0f)
            {
              
            }
        }
    }
}
