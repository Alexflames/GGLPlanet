﻿using UnityEngine;
using Mirror;

public class PlayerAbilityManager : NetworkBehaviour
{
    private PlayerAbility ability;

    private void Start()
    {
        energyMax = 100;
        energy = energyMax;
        energyGainPerSecond = 5;
        ability = new SpeedUpAbility();
        ability.Init();
        UpdateStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (ability.IsActive == false)
            {
                if (energy > ability.EnergyCost)
                {
                    energy -= ability.EnergyCost;
                    ability.Activate();
                    UpdateStats();
                }
            }
            else
            {
                ability.Deactivate();
                UpdateStats();
            }
        }
    }

    private void FixedUpdate()
    {
        energy = Mathf.Min(energy + energyGainPerSecond / Time.fixedDeltaTime);
        ability.FixedUpdateCall();
    }

    private void OnDisable()
    {
        ability.ForceBreak();
        UpdateStats();
    }

    private void UpdateStats()
    {
        statMod.Reset();
        ability.UpdateStats(statMod);
    }

    private float energy;
    private float energyMax;
    private float energyGainPerSecond;
    private PlayerStatMod statMod;
}