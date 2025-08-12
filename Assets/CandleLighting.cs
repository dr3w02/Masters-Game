using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

public class CandleLighting : MonoBehaviour
{

    public List<GameObject> lights;
    //= new List<GameObject>();
    public float turnOffDelay = 0f;
    public bool isTurningOff = false;
    public SanityScore sanity;


    //To change this make it a light when score is above a certain amount and everytime the player messes up the score gose up...

    void Update()
    {

        startlight();

        lights = new List<GameObject>(Resources.LoadAll<GameObject>("lights"));

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
        Debug.Log("Lights number" + (lights.Count));
        if (isTurningOff == true)
        {
            if (lights.Count > 0)
            {
                int Lightsinlist = lights.Count;

                GameObject lightRand = lights[Random.Range(0, Lightsinlist)];

                    if (lightRand.activeSelf)
                    {
                        
                        TurnOffCandle(lightRand);
                        lights.Remove(lightRand);
                        Debug.Log("Randomly chosen candle: " + lightRand);


                    }
                    


                


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
        
       

    }
}
