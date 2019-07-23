using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseOfItems
{
    Dictionary<string, ItemBase> DataBaseItems = new Dictionary<string, ItemBase>();

    public void DataBase(string Id, out ItemBase CurrentItem)
    {
        AddItemsInDataBase();
        foreach( KeyValuePair<string, ItemBase> i in DataBaseItems)
        {
            if (Id == i.Key)
            {
                CurrentItem = i.Value;
                return;
            }
        }
        CurrentItem = null;
    }

    private void AddItemsInDataBase()
    {
        DataBaseItems.Add("HealthBottle", new HpBottle());
    }
}
