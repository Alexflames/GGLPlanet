using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : StatsManager
{
    protected override void Dies()
    {
        gameObject.GetComponent<SimplePlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerShooting>().enabled = false;
        this.enabled = false;
    }
}
