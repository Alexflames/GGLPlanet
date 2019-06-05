using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerStatsManager : StatsManager
{
    Slider hp;

    protected override void StatsStart()
    {
        hp = GameObject.Find("HPBar").GetComponent<Slider>();
        hp.maxValue = MaxHP;
        hp.value = CurrentHP;
    }

    protected override void HPChange(int hp)
    {
        this.hp.value = CurrentHP;
    }

    protected override void Dies()
    {
        gameObject.GetComponent<SimplePlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerShooting>().enabled = false;
        this.enabled = false;
    }
}
