
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    private KeyCode collectItems = KeyCode.F;

    public Inventory Inventory1;

    private Collider2D collider2d;

    private List<Collider2D> Collider2Ds = new List<Collider2D>();

    private void Start()
    {
        Inventory1 = GetComponentInParent<Inventory>();
    }

   private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemBase item = collider.GetComponent<ItemBase>();
        if(item)
        {
            Collider2Ds.Add(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider)
        {
            ItemBase item = collider.GetComponent<ItemBase>();
            if (item)
            {
                for (int i = 0; i < Collider2Ds.Count; i++)
                {
                    if (Collider2Ds[i] == collider)
                    {
                        Collider2Ds.RemoveAt(i);
                    }
                }
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(collectItems))
        {
            if (Collider2Ds.Count > 0)
            {
                ItemBase item = Collider2Ds[0].GetComponent<ItemBase>();
                Inventory1.NewItem = item;
                Destroy(Collider2Ds[0].gameObject);
            }
        }
    }

}
