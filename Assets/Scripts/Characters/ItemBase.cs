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
   
    public virtual void Activate(GameObject player)
    {

    }
}
