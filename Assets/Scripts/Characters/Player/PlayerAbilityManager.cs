using UnityEngine;
using Mirror;

public class PlayerAbilityManager : NetworkBehaviour
{
    private PlayerAbility ability;
    
    private PlayerStatsManager statsManager;

    private void Start()
    {
        statsManager = GetComponent<PlayerStatsManager>();
        energyMax = 100;
        energy = energyMax;
        energyGainPerSecond = 5;
        ability = new SpeedUpAbility();
        ability.Init();
        ability.PassTarget(statsManager);
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
                }
            }
            else
            {
                ability.Deactivate();
            }
        }
    }

    private void FixedUpdate()
    {
        energy = Mathf.Min(energy + energyGainPerSecond * Time.fixedDeltaTime, energyMax);
        ability.FixedUpdateCall();
    }

    private void OnDisable()
    {
        ability.ForceBreak();
    }

    private float energy;
    private float energyMax;
    private float energyGainPerSecond;
    
}
