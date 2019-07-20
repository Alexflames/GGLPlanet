using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBase
{
    public virtual string NameOfItem { get { return ""; } }

    public virtual string id { get { return ""; } }

    public virtual int countItem { get { return 0; } set { } }

    public virtual bool IsStackable { get { return false; } }

    public virtual string DescriptionItem { get { return ""; } }

    public virtual string pathIcon { get { return ""; } }

    public virtual string pathPrefab { get { return ""; } }
    /* public string id = "";
     public virtual string ID
     {
         get
         {
             return id;
         }
     }*/


    /*public int countItem = 0;
    public virtual int CountItem
    {
        get
        {
            return countItem;
        }
    }*/


    /*public bool IsStackable = false;
    public virtual bool isstackable
    {
        get
        {
            return IsStackable;
        }
    }*/

    /*public string DescriptionItem = "";
    public virtual string Descriptionitem
    {
        get
        {
            return DescriptionItem;
        }
    }*/

    /*public string pathIcon = "";
    public virtual string PathIcon
    {
        get
        {
            return pathIcon;
        }
    }

    public string pathPrefab = "";
    public virtual string PathPrefab
    {
        get
        {
            return pathPrefab;
        }
    }*/

    public virtual void Activate(GameObject player)
    {

    }
}
