using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FadingObject : MonoBehaviour, IEquatable<FadingObject>
{

    public List<Renderer> Renderers = new List<Renderer>();
    public Vector3 _position;
    public List<Material> Materials = new List<Material>();

  
    public float InitialAlpha = 1;

    public float fadeDuration = 1f;


    public Animator nextSceneAnim;


    private void Awake()
    {
        _position = transform.position;

        Materials.Clear();

        foreach (Renderer renderer in Renderers)
        {
            Materials.AddRange(renderer.materials);
        }
            

        InitialAlpha = Materials[0].color.a;
    }

    public bool Equals(FadingObject other)
    {
        return _position.Equals(other._position);
    }
    public void SetIntensity(float value)
    {
        float targetAlpha = Mathf.Clamp01(value);

        foreach (Material mat in Materials)
        {

            Color colour = mat.color;
            colour.a = targetAlpha;
            mat.color = colour;
        }
    }

    public override int GetHashCode()
    {
        return _position.GetHashCode();
    }


    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            SetIntensity(Mathf.Lerp(InitialAlpha, 0f, elapsed / fadeDuration));
            yield return null;
        }
        SetIntensity(0f);

        nextSceneAnim.Play("GhostAnim");
    }
}
