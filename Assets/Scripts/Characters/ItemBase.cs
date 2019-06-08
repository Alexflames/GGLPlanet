using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public string NameOfItem;

    public int id;

    public int countItem;

    public bool IsStackable;

    [Multiline(4)]
    public string DescriptionItem;

    public string pathIcon;

    public string pathPrefab;
}
