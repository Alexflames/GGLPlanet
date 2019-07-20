using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsOnTheGround : MonoBehaviour
{
    private ItemBase CurrentItem;

    void Start()
    {
        CurrentItem = new HpBottle();
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
