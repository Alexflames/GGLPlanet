using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsOnTheGround : MonoBehaviour
{
    private ItemBase CurrentItem;

    public string ID = "";

    DataBaseOfItems DataBase = new DataBaseOfItems();

    void Start()
    {
        DataBase.DataBase(ID, out CurrentItem);
    }

    public void Collect(Inventory inventory)
    {
        if(CurrentItem.IsStackable)
        {
            inventory.CollectStackableItem(CurrentItem);
        }
        else
        {
            inventory.CollectItems(CurrentItem);
        }
        Destroy(gameObject);
    }
}
