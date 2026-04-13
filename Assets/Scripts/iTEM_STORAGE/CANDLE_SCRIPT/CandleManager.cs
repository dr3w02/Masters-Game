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
    public List<GameObject> litCandles = new List<GameObject>();
    public float turnOffDelay;
    public bool isTurningOff = false;
    public SanityScore sanity;

    public FadingObject fading;

    CandleLighting candleLighting;

    public bool gameStarted;
   

    void Start()
    {
         candleLighting = FindAnyObjectByType<CandleLighting>();
        //move this
        StartCandles();


    }

    public void CheckAllLit()
    {

      


        if (litCandles.Count >= Candles.Count)
        {
            if (!gameStarted)
            {
                fading.FadeOut();
                gameStarted = true;
            }
            else
            {
                StartCoroutine(LightTurnOffRoutine());
            }
           

           
           
            Debug.Log("all candles lit!");
        }
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

        if (litCandles.Count > 0)
        {
            GameObject randFlame = litCandles[Random.Range(0, litCandles.Count)];

            // Reset the isLit flag on the owning candle
            CandleLighting owningCandle = randFlame.GetComponentInParent<CandleLighting>();
            if (owningCandle != null)
                owningCandle.isLit = false;

            randFlame.SetActive(false);
            litCandles.Remove(randFlame);

            if (litCandles.Count < 4)
            {
                Debug.Log("Game Over");
                sanity.sanityDecrease = true;
            }
            else
            {
                sanity.sanityDecrease = false;
            }
        }



        StartCoroutine(LightTurnOffRoutine());




    }



  
    public void TurnOffCandleLight(GameObject light)
    {
        light.SetActive(false);
    }
}



