using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemBase> items;

    public GameObject cellContainer;

    private KeyCode showInventory = KeyCode.I;

    private GameObject player;
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
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
        else
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
    }

}
