using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBottle : ItemsConsumble
{
    private StatsManager StatsManager;
    public override string NameOfItem
    {
        get
        {
            return "HealthBottle";
        }
    }

    public override string id
    {
        get
        {
            return "HealthBottle";
        }
    }

    public int CountItem = 1;

    public override int countItem
    {
        get
        {
            return CountItem;
        }
        set
        {
            CountItem = value;
        }
    }

    public override string DescriptionItem
    {
        get
        {
            return "Drink me";
        }
    }

    public override bool IsStackable
    {
        get
        {
            return true;
        }
    }

    public override string pathIcon 
    {
        get
        {
            return "Heal";
        }
    }

    public override string pathPrefab 
    {
        get
        {
            return "HealthBottle";
        }
    }

    protected override void ActivateEffect(GameObject player)
    {
        
        StatsManager = player.GetComponent<StatsManager>();
        AttackInformation attackInformation = new AttackInformation(player,-1);
        StatsManager.DealDamage(attackInformation);

    }
}
