using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using UnityEngine;


public class ItemStorage : MonoBehaviour
{


    public List<InventoryInfo> collected = new List<InventoryInfo>();


    //public void Awake()
    //{
    //    List<ItemStorage> inventory = new List<ItemStorage>();

    //}
  
 
}


[System.Serializable]
public class InventoryInfo
{
    public string Name;
    public int Quantity;
    public GameObject GameObject;
    
}
