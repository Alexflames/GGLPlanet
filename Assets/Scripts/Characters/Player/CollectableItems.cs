
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    private Inventory Inventory1;

    private List<ItemBase> ItemBases = new List<ItemBase>();

    private void Start()
    {
        Inventory1 = GetComponentInParent<Inventory>();
    }

   private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemBase item = collider.GetComponent<ItemBase>();
        if(item)
        {
            ItemBases.Add(item);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider)
        {
            ItemBase item = collider.GetComponent<ItemBase>();
            if (item)
            {
                for (int i = 0; i < ItemBases.Count; i++)
                {
                    if (ItemBases[i] == item)
                    {
                        ItemBases.RemoveAt(i);
                    }
                }
            }
        }
    }

    public void Control()
    {
        if (ItemBases.Count > 0)
        {
            ItemBase item = ItemBases[0];
            Inventory1.CollectItems(item);
            Destroy(ItemBases[0].gameObject);
        }
    }

}
