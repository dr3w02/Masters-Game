using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectIntensity : MonoBehaviour
{

    public Volume chromaticIntensity;

   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chromaticIntensity = GetComponent<Volume>();
      
    }

    public void SetIntensity(float value)
    {
        chromaticIntensity.weight = Mathf.Clamp01(value);
    }
}

