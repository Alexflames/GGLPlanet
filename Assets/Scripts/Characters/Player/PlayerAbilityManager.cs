using UnityEngine;
using Mirror;

public class PlayerAbilityManager : NetworkBehaviour
{
    [SerializeField]
    private PlayerAbility ability;

    private void Start() {
        energyMax = 100;
        energy = energyMax;
        energyGainPerSecond = 5;
    }

    private void Update() {
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

    private void FixedUpdate() {
        energy += energyGainPerSecond / Time.fixedDeltaTime;
        if (energy > energyMax)
        {
            energy = energyMax;
        }
    }

    private void OnDisable() {
        ability.ForceBreak();
    }

    private double energy;
    private double energyMax;
    private double energyGainPerSecond;
}
