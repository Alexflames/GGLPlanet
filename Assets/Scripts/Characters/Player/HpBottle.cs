using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBottle : ItemsConsumble
{
    private StatsManager StatsManager;
    private GameObject HpBottle1;
    protected override void ActivateEffect(GameObject player)
    {
        StatsManager = player.GetComponent<StatsManager>();
        AttackInformation attackInformation = new AttackInformation(player,-1);
        StatsManager.DealDamage(attackInformation);

    }
}
