using JetBrains.Annotations;
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

    public bool doorClosed;

    public bool gameStarted;

    [Header("Audio")]
    public AudioSource candlesAudio;
    public bool prompted;
    public bool mainScene;

    void Start()
    {
        gameStarted = false;
        doorClosed = false;

        candlesAudio = FindAnyObjectByType<AudioSource>();
        //move this
        StartCandles();
        prompted = false;
        if (mainScene)
        {
            StartCoroutine(LightTurnOffRoutine());

        }
        else
        {
            litCandles.Clear();
            CheckAllLit();
        }

    }


    public void CheckAllLit()
    {
        if (litCandles.Count >= Candles.Count)
        {
            if (!gameStarted)
            {
                if (doorClosed)
                {
                    fading.FadeOut();

                    gameStarted = true;
                }
               

            }
         


        }

        //if (gameStarted)
        //{
        //    StartCoroutine(LightTurnOffRoutine());
        //}
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

    //public void TurnOffCandle()
    //{
    //    Debug.Log("PlaceCandles");
    //    InvokeRepeating("TurnOffCandleCycle", 5f, 5f);

    //    bool allCandlesActive = Candles.All(c => c.activeSelf);

    //    if (!allCandlesActive)
    //    {
    //        // Sanity score decrease
    //    }
    //}

    public IEnumerator LightTurnOffRoutine()
    {
        yield return new WaitForSeconds(turnOffDelay);

        if (litCandles.Count > 0)
        {
            GameObject randFlame = litCandles[Random.Range(0, litCandles.Count)];

            
            CandleLighting owningCandle = randFlame.GetComponentInParent<CandleLighting>();
            if (owningCandle != null)
                owningCandle.isLit = false;

            randFlame.SetActive(false);
            litCandles.Remove(randFlame);

            if (litCandles.Count < 4)
            {
                
                sanity.sanityDecrease = true;

                if (!prompted)
                {
                    candlesAudio.Play();
                    prompted = true;
                  
                }
                if (prompted)
                {
                    candlesAudio.Stop();
                }
               
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



