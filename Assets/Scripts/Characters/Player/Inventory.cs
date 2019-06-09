using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemBase> items;

    public GameObject cellContainerPrefab;

    private KeyCode showInventory = KeyCode.I;

    private GameObject player;

    private GameObject cellContainer;

    private GameObject Canvas;

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
        cellContainer = Instantiate(cellContainerPrefab, Canvas.transform.position, Quaternion.identity) as GameObject;
        cellContainer.transform.SetParent(Canvas.transform);

        items = new List<ItemBase>();
        for(int i = 0;i < cellContainer.transform.childCount ;i++)
        {
            items.Add(new ItemBase());
        }
        cellContainer.SetActive(false);
    }

    private void Update()
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
