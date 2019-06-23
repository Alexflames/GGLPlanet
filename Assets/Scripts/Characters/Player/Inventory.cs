using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<ItemBase> items;

    private KeyCode showInventory = KeyCode.I;
    private KeyCode collectItems = KeyCode.F;

    private GameObject cellContainer;
    private GameObject Canvas;
    public GameObject cellContainerPrefab;

    private CollectableItems CollectableItems;

    private void Start()
    {
        CollectableItems = GetComponentInChildren<CollectableItems>();

        Canvas = GameObject.Find("Canvas");
        cellContainer = Instantiate(cellContainerPrefab, Canvas.transform.position, Quaternion.identity) as GameObject;
        cellContainer.transform.SetParent(Canvas.transform);

        items = new List<ItemBase>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        { 
            items.Add(cellContainer.transform.GetChild(i).GetComponent<ItemBase>());
        }
        cellContainer.SetActive(false);
    }

    private void Update()
    {
        KeyDown();
    }

    private void KeyDown()
    {
        if (Input.GetKeyDown(showInventory))
        {
            if (cellContainer.activeSelf)
            {
                cellContainer.SetActive(false);
            }
            else
            {
                cellContainer.SetActive(true);
            }
        }
        if(Input.GetKeyDown(collectItems))
        {
            CollectableItems.Control();
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

    private void DisplayItems()
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
                if(items[i].IsStackable)
                {
                    txt.enabled = true;
                    txt.text = items[i].countItem.ToString();
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
