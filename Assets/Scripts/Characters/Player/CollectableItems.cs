
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItems : MonoBehaviour
{
    private Inventory Inventory1;

    private List<ItemsOnTheGround> ItemsOnTheGround = new List<ItemsOnTheGround>();

    private void Start()
    {
        Inventory1 = GetComponentInParent<Inventory>();
    }

   private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemsOnTheGround item = collider.GetComponent<ItemsOnTheGround>();
        if(item)
        {
            ItemsOnTheGround.Add(item);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {

            ItemsOnTheGround item = collider.GetComponent<ItemsOnTheGround>();
            if (item)
            {
                for (int i = 0; i < ItemsOnTheGround.Count; i++)
                {
                    if (ItemsOnTheGround[i] == item)
                    {
                    ItemsOnTheGround.RemoveAt(i);
                    }
                }
            }
        
    }

    public void Control()
    {
        if (ItemsOnTheGround.Count > 0)
        {
            ItemsOnTheGround item = ItemsOnTheGround[0];
            AddItem(item);
        }
    }

    private void AddItem(ItemsOnTheGround item)
    {
        item.Collect(Inventory1);
    }

}
