
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItems : MonoBehaviour
{
    private Inventory Inventory1;

    public GameObject MessageManagerPrefab;
    private GameObject MessageManager;
    private GameObject Canvas;
    public GameObject message;

    private List<ItemBase> ItemBases = new List<ItemBase>();

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
        MessageManager = Instantiate(MessageManagerPrefab, new Vector3(150,270,0), Quaternion.identity) as GameObject;
        MessageManager.transform.SetParent(Canvas.transform);

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
            Debug.Log(item.NameOfItem);
            Message(item);
            AddItem(item);
            Destroy(ItemBases[0].gameObject);
        }
    }

    private void Message(ItemBase item)
    {
        GameObject msgObj = Instantiate(message);
        msgObj.transform.SetParent(MessageManager.transform);
        Text msgtxt = msgObj.transform.GetChild(1).GetComponent<Text>();
        msgtxt.text = item.NameOfItem;
        Image msgimg = msgObj.transform.GetChild(0).GetComponent<Image>();
        msgimg.sprite = Resources.Load<Sprite>(item.pathIcon);
    }

    private void AddItem(ItemBase item)
    {
        if (!item.IsStackable)
        {
            Inventory1.CollectItems(item);
        }
        else
        {
            Inventory1.CollectStackableItem(item);
        }
    }

}
