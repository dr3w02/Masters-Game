using System;
using UnityEngine;
using System.Collections.Generic;

public class FadingObject : MonoBehaviour, IEquatable<FadingObject>
{

    public List<Renderer> Renderers = new List<Renderer>();
    public Vector3 _position;
    public List<Material> Materials = new List<Material>();

  
    public float InitialAlpha = 1;

    private void Awake()
    {
        _position = transform.position;

        if(Renderers.Count == 0)
        {
            Renderers.AddRange(GetComponentsInChildren<Renderer>());
        }

        //foreach(Renderer renderer in Renderers)
        //{
        //    Materials.AddRange(renderer.materials);
        //}

        InitialAlpha = Materials[0].color.a;
    }

    public bool Equals(FadingObject other)
    {
        return _position.Equals(other._position);
    }
    public void SetIntensity(float value)
    {
        InitialAlpha = Mathf.Clamp01(value);

        foreach (Material mat in Materials)
        {

            Color colour = Materials[0].color;
            colour.a = InitialAlpha;
            Materials[0].color = colour;
        }
    }

    public override int GetHashCode()
    {
        return _position.GetHashCode();
    }
}
