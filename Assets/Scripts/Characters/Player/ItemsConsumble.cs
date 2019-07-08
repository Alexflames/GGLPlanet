using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemsConsumble : ItemBase
{
    public override void Activate(GameObject player)
    {
        ActivateEffect(player);
    }
    protected virtual void ActivateEffect(GameObject palyer)
    {
    }
}
