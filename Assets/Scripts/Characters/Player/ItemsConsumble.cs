using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemsConsumble : ItemBase
{
    public override void Activate(PointerEventData eventData,int index,Inventory inventory, GameObject player)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inventory.items[index].id != 0)
            {
                GameObject droppedObject = Instantiate(Resources.Load<GameObject>(inventory.items[index].pathPrefab));
                droppedObject.transform.position = player.transform.position + Vector3.right * 0.5f;
                ItemsReduction(inventory, index);
            }
        }
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (inventory.items[index].id != 0)
            {
                ActivateEffect(player);
                ItemsReduction(inventory, index);
            }
        }
    }
    private void ItemsReduction(Inventory inventory,int index)
    {
        if (inventory.items[index].countItem > 1)
        {
            inventory.items[index].countItem--;
        }
        else
        {
            inventory.items[index] = new ItemBase();
        }
        inventory.DisplayItems();
    }
    protected virtual void ActivateEffect(GameObject palyer)
    {
    }
}
