using UnityEngine;

public class SpeedUpAbility : PlayerAbility
{
    private int energyCost = 50;

    private float abilityDuration = 5;

    private float abilityCooldownPeriod = 0.5f;

    private float speedMul = 1.2f;

    public override void Init()
    {
        abilityCooldownRemaining = 0;
        isActive = false;
    }

    public override void UserPress()
    {
        if (!isActive && abilityCooldownRemaining <= 0 && user.Energy >= energyCost)
        {
            user.DrainEnergy(energyCost);
            TurnOn();
        }
    }

    public override void FixedUpdateCall()
    {
        if (abilityDurationRemaining > 0)
        {
            abilityDurationRemaining -= Time.fixedDeltaTime;
        }
        else 
        {
            if (isActive)
            {
                TurnOff();
                abilityCooldownRemaining = abilityCooldownPeriod;
            }
            else
            {
                if (abilityCooldownRemaining > 0)
                {
                    abilityCooldownRemaining -= Time.fixedDeltaTime;
                }
            }
        }
    }

    public override void ForceBreak()
    {
        TurnOff();
    }

    public override bool IsActive
    {
        get { return isActive; }
    }

    public override float CooldownRemaining
    {
        get { return abilityCooldownRemaining; }
    }

    public override int EnergyCost
    {
        get { return energyCost; }
    }

    private bool isActive = false;
    private float abilityDurationRemaining = 0;
    private float abilityCooldownRemaining = 0;

    private void TurnOn()
    {
        isActive = true;
        user.SpeedAddMul(speedMul);
        abilityDurationRemaining = abilityDuration; 
    }

    private void TurnOff()
    {
        isActive = false;
        user.SpeedAddMul(1 / speedMul);
    }
}
