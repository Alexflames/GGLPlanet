using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public List<ItemBase> items;

    private KeyCode showInventory = KeyCode.I;
    private KeyCode collectItems = KeyCode.F;

    private GameObject cellContainer;
    private GameObject Canvas;
    public GameObject cellContainerPrefab;

    private CollectableItems сollectableItems;

    private void Start()
    {
        сollectableItems = GetComponentInChildren<CollectableItems>();

        Canvas = GameObject.Find("Canvas");
        cellContainer = Instantiate(cellContainerPrefab, Canvas.transform.position, Quaternion.identity) as GameObject;
        cellContainer.transform.SetParent(Canvas.transform);

        items = new List<ItemBase>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        { 
            items.Add(cellContainer.transform.GetChild(i).GetComponent<ItemBase>());
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
                if (items[i].id == 0)
                {
                    items[i] = newItem;
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
            if (items[i].id != 0)
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
        
}
