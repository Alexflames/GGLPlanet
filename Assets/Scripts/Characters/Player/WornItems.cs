using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornItems : ItemBase
{
    public override void Activate(GameObject player)
    {
        PutOnItem(player);
    }
    protected virtual void PutOnItem(GameObject palyer)
    {
    }
}
