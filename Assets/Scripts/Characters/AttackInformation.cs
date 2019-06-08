using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInformation
{
    public GameObject owner;
    public int damage;

    public AttackInformation(GameObject owner, int damage)
    {
        this.owner = owner;
        this.damage = damage;
    }

};
