using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class CandleLighting : MonoBehaviour
{
    CandleManager candleManager;

    [SerializeField] private GameObject flame;

    public bool isLit;

   


    public void Start()
    {
        candleManager = FindAnyObjectByType<CandleManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter")&& !isLit)
        {
          
            
            LightCandle();
        }


    }

    public void LightCandle()
    {
        isLit = true;
        flame.SetActive(true);
        candleManager.lights.Add(flame);

    }

    public void ExtinguishCandle()
    {
        isLit = false;
        flame.SetActive(false);
    }
}