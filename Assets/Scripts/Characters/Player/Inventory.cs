using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<ItemBase> items;

    public GameObject cellContainerPrefab;

    private KeyCode showInventory = KeyCode.I;

    private GameObject player;

    private GameObject cellContainer;

    private GameObject Canvas;

    private ItemBase newItem = null;

    public ItemBase NewItem
    { set
        {
            newItem = value;
            CollectItems();
        }
    }

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
        cellContainer = Instantiate(cellContainerPrefab, Canvas.transform.position, Quaternion.identity) as GameObject;
        cellContainer.transform.SetParent(Canvas.transform);

        items = new List<ItemBase>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            items.Add(new ItemBase());
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
    }

    private void CollectItems()
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
            Image img = icon.GetComponent<Image>();
            if (items[i].id != 0)
            {
                img.sprite = Resources.Load<Sprite>(items[i].pathIcon);
                img.enabled = true;
            } 
            else
            {
                img.enabled = false;
                img.sprite = null;
            }
        }
    }
        
}
