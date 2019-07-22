using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseOfItems : MonoBehaviour
{
    public void DataBase(string Id, out ItemBase CurrentItem)
    {
        switch(Id)
        {
                case "HealthBottle":
                CurrentItem = new HpBottle();
                break;
                default:
                CurrentItem = null;
                break;
        }

    }
}
