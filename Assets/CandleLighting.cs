using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class CandleLighting : MonoBehaviour
{
    
    public List<GameObject> lights = new List<GameObject>();
    private float turnOffDelay = 8f;
    public bool isTurningOff = false;


    //To change this make it a light when score is above a certain amount and everytime the player messes up the score gose up...

    void Update()
    {

            startlight();
    }

    public void startlight()
    {
        if (!isTurningOff)
        {
            StartCoroutine(LightTurnOffRoutine());
        }
    }


     IEnumerator LightTurnOffRoutine()
    {
        isTurningOff = true;

        if (isTurningOff == true)
        {
            if (lights.Count > 0)
            {
                int Lightsinlist = lights.Count;

                GameObject lightRand = lights[Random.Range(0, Lightsinlist)];

                    if (lightRand.activeSelf)
                    {
                        
                        TurnOffCandle(lightRand);
                        Debug.Log("Randomly chosen candle: " + lightRand);

                    }


                


            }

            

            if (lights.Count == 0)
            {
                Debug.Log("Game Over");
               
            }


            

            yield return new WaitForSeconds(turnOffDelay);

            

            Stop();


        }

     

    }
    public void Stop()
    {
        Debug.Log("TurnOff");

        isTurningOff = false;

        if (isTurningOff == false)
        {
            StopCoroutine(LightTurnOffRoutine());

        }
  
    }

    public void TurnOffCandle(GameObject light)
    {

        light.SetActive(false);

        //int LightIndex = Random.Range(0, lights.Count);

        //if (lights != null) // Check if the object exists and is currently active
        //{
        //    // Deactivate the object
        //    Debug.Log(lights.Count + "was active and has been turned off.");
        //    lights[lights.Count].SetActive(false);
        //}

    }
}
