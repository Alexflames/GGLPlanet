using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBase:MonoBehaviour
{
    public string NameOfItem = "";

    public int id = 0;

    public int countItem = 0;

    public bool IsStackable = false;

    public string DescriptionItem = "";

    public string pathIcon = "";

    public string pathPrefab = "";

    public virtual void Activate(GameObject player)
    {

    }
}
