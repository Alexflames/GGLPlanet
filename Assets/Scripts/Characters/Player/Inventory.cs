using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public List<ItemBase> items;

    private KeyCode showInventory = KeyCode.I;
    private KeyCode collectItems = KeyCode.F;

    private GameObject cellContainer;
    private GameObject Canvas;
    public GameObject cellContainerPrefab;
    public GameObject MessageManagerPrefab;
    private GameObject MessageManager;
    public GameObject message;

    private CollectableItems сollectableItems;

    private void Start()
    {
        сollectableItems = GetComponentInChildren<CollectableItems>();

        Canvas = GameObject.Find("Canvas");

        cellContainer = Instantiate(cellContainerPrefab, Canvas.transform.position, Quaternion.identity) as GameObject;
        cellContainer.transform.SetParent(Canvas.transform);

        MessageManager = Instantiate(MessageManagerPrefab, new Vector3(150, 270, 0), Quaternion.identity) as GameObject;
        MessageManager.transform.SetParent(Canvas.transform);

        items = new List<ItemBase>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            items.Add(new ItemBase());
        }
        cellContainer.SetActive(false);

        for(int i = 0;i < cellContainer.transform.childCount;i++)
        {
            cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
        }
    }

    private void Update()
    {
        KeyDown();
    }

    private void KeyDown()
    {
        if (Input.GetKeyDown(showInventory))
        {
            cellContainer.SetActive(!cellContainer.activeSelf);
        }
        if(Input.GetKeyDown(collectItems))
        {
            сollectableItems.Control();
        }
    }

     public void CollectItems(ItemBase newItem)
    { 
        if (newItem != null)
        {
            for (int i = 0;i < items.Count;i++)
            {
                if (items[i].id == "")
                {
                    items[i] = newItem;
                    Message(newItem);
                    DisplayItems();
                    newItem = null;
                    break;
                }
            }
        }
    }

    public void CollectStackableItem(ItemBase newItem)
    {
        if (newItem != null)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].id == newItem.id)
                {
                    items[i].countItem++;
                    Message(newItem);
                    DisplayItems();
                    newItem = null;
                    return;
                }
            }
            CollectItems(newItem);
        }

    }

    public void DisplayItems()
    {
        for(int i = 0;i < items.Count;i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform countItem = icon.GetChild(0);
            Text txt = countItem.GetComponent<Text>();

            Image img = icon.GetComponent<Image>();
            if (items[i].id != "")
            {
                img.sprite = Resources.Load<Sprite>(items[i].pathIcon);
                img.enabled = true;
                if(items[i].countItem > 1)
                {
                    txt.text = items[i].countItem.ToString();
                }
                else
                {
                    txt.text = null;
                }
            } 
            else
            {
                img.enabled = false;
                img.sprite = null;
            }
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
    
    public void PointerEffect(PointerEventData eventData, int index)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (items[index].id != "")
            {
                Drop(index);
                ItemsReduction(index);
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (items[index].id != "")
            {
                items[index].Activate(gameObject);
                ItemsReduction(index);
            }
        }
    }

    private void Drop(int index)
    {
        GameObject droppedObject = Instantiate(Resources.Load<GameObject>(items[index].pathPrefab));
        droppedObject.transform.position = gameObject.transform.position + Vector3.right * 0.5f;
    }

    private void ItemsReduction(int index)
    {
        if (items[index].countItem > 1)
        {
            items[index].countItem--;
        }
        else
        {
           items[index] = new ItemBase();
        }
       DisplayItems();
    }
}
