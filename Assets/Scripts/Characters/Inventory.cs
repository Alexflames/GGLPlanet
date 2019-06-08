using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemBase> items;

    public GameObject cellContainer;

    private KeyCode showInventory = KeyCode.I;
    private void Start()
    {
        cellContainer.SetActive(false);
        items = new List<ItemBase>();
        for(int i = 0;i < cellContainer.transform.childCount ;i++)
        {
            items.Add(new ItemBase());
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(showInventory))
        {
            if(cellContainer.activeSelf)
            {
                cellContainer.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                cellContainer.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

}
