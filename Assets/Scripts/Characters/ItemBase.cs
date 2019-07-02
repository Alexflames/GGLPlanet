using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public enum ItemType
    {
        Ammunition,
        UsableItem,
        Clothes
    }

    public string NameOfItem = "";

    public int id = 0;

    public int countItem = 0;

    public bool IsStackable = false;

    [Multiline(4)]
    public string DescriptionItem = "";

    public string pathIcon = "";

    public string pathPrefab = "";

    public ItemType Itemtype;
}
