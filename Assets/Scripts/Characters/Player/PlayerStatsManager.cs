using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerStatsManager : StatsManager
{
    Slider hp;

    public float Energy { get { return energy; } }

    public void DrainEnergy(float amount)
    {
        energy -= amount;
    }

    private float energyMax = 100;
    private float energy;
    private float energyGainPerSecond = 5;
    protected override void StatsStart()
    {
        energy = energyMax;
        hp = GameObject.Find("HPBar").GetComponent<Slider>();
        hp.maxValue = MaxHP;
        hp.value = CurrentHP;
    }

    protected override void StatsFixedUpdate()
    {
        energy = Mathf.Min(energy + energyGainPerSecond * Time.fixedDeltaTime, energyMax);
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
