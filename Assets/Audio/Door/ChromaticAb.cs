using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaticAb : MonoBehaviour
{
    private PostProcessVolume _postProcessVolume;
    private ChromaticAberration _chromaticAb;

    [SerializeField] private TextureParameter _spectralLut;


    private void Start()
    {
        _postProcessVolume  = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings(out _chromaticAb);
    }


    public void ChromaticAbberationOnOff(bool on)
    {
        if (on)
        {
            _chromaticAb.active = true;
        }
        else
        {
            _chromaticAb.active = false;
        }
    }

    public void ChromaticAbberationLUT()
    {
        _chromaticAb.spectralLut = _spectralLut;
    }

    public void Intensity(float sliderValue)
    {
        _chromaticAb.intensity.value = sliderValue;
    }

    public void SetFastMode(bool on)
    {
        if (on)
        {
            _chromaticAb.fastMode.value = true;
        }
        else
        {
            _chromaticAb.fastMode.value = false;
        }
    }


}
