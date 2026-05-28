using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadingObject : MonoBehaviour, IEquatable<FadingObject>
{

    public List<Renderer> Renderers = new List<Renderer>();
    public Vector3 _position;
    public List<Material> Materials = new List<Material>();


    public float InitialAlpha = 1;

    public float fadeDuration = 5f;



    public GameObject whiteChalk;

    public GameObject particalEffect;
    public FadeScreen Fade;

    public GameObject ghosts;
    private void Awake()
    {
        particalEffect.SetActive(false);
        _position = transform.position;

        Materials.Clear();

        foreach (Renderer renderer in Renderers)
        {
            Materials.AddRange(renderer.materials);
        }


        whiteChalk.SetActive(true);

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
        
        particalEffect.SetActive(true);

        ghosts.SetActive(true);

        float elapsed = 0f;
        SetIntensity(0f);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float intensity = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            SetIntensity(intensity);

            if (intensity >= 1f)
            {
                whiteChalk.SetActive(false);

            }


            yield return null;
        }



        SetIntensity(1f);

        whiteChalk.SetActive(false);


        Fade.FadeOut();

        yield return new WaitForSeconds(5f);
        ghosts.SetActive(false);

        SceneManager.LoadScene("Main Game");
    }
}
