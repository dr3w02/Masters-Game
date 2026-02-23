using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CandleManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> Candles = new List<GameObject>();

    // From CandleLighting
    public List<GameObject> lights;
    public float turnOffDelay;
    public bool isTurningOff = false;
    public SanityScore sanity;

    void Start()
    {
        //move this
        StartCandles();
        StartCoroutine(LightTurnOffRoutine());

    }

  
    public void StartCandles()
    {
        Candles.Clear();

        foreach (Transform child in transform)
        {
            Candles.Add(child.gameObject);
        }
        foreach (GameObject candle in Candles)
        {
            candle.SetActive(true);
        }
    }

    public void TurnOffCandle()
    {
        Debug.Log("PlaceCandles");
        InvokeRepeating("TurnOffCandleCycle", 5f, 5f);

        bool allCandlesActive = Candles.All(c => c.activeSelf);

        if (!allCandlesActive)
        {
            // Sanity score decrease
        }
    }

 
   
    public IEnumerator LightTurnOffRoutine()
    {
        yield return new WaitForSeconds(turnOffDelay);

      
            Debug.Log("Lights number" + lights.Count);

            int lightsInList = lights.Count;
            GameObject lightRand = lights[Random.Range(0, lightsInList)];

            if (lightRand.activeSelf)
            {
                TurnOffCandleLight(lightRand);
                lights.Remove(lightRand);
                Debug.Log("Randomly chosen candle: " + lightRand);
            }


            if (lights.Count < 4)
            {
                Debug.Log("Game Over");
                sanity.sanityDecrease = true;
            }
            
            else
            {
               sanity.sanityDecrease = false;
            }

            StartCoroutine(LightTurnOffRoutine());




    }



  
    public void TurnOffCandleLight(GameObject light)
    {
        light.SetActive(false);
    }
}



