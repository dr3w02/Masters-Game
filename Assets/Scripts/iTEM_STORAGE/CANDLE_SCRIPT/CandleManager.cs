using Oculus.Interaction.Locomotion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CandleManager : MonoBehaviour
{

    //public ItemStorage items;

    [SerializeField]
    private List<GameObject> Candles = new List<GameObject>();

   

    void Start()
    {
        StartCandles();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Lighter"))
        {
            
        }
    }
   

    public void StartCandles()
    {
        Candles.Clear();


        foreach (Transform child in transform)
        {
            Candles.Add(child.gameObject);

        }
        foreach (GameObject Candles in Candles)
        {
            Candles.SetActive(true);
        }

    }

    public void TurnOffCandle()
    {
        Debug.Log("PlaceCandles");
        //InventoryInfo candle = items.collected.Find(i => i.Name == "Candle");

        // First, activate all candles
        InvokeRepeating("TurnOffCandleCycle", 5f,5f);


        bool allCandlesActive = Candles.All(c => c.activeSelf);

        if (!allCandlesActive)
        {
            //Sanity score decrease


        }
    }
    public void TurnOffCandleCycle()
    {

    }
}




//int index = Candles.Count + 1;

//for (int i = 0; i < candle.Quantity && i < Candles.Count; i++)
//{
//    Candles[i].SetActive(false);
//    //turns off candle if its in the list 
//    //Candles.RemoveAt(i);
//}




//private int CandleDecrese()
//{
//    while (true)
//    {
//        for (int i = 0; i < Candles.Count; i++)
//        {
//            if (Candles.activeSelf == true)
//            {

//            }
//        }
//    }
//}


