using UnityEngine;

public class MedkitAbility : PlayerAbility
{
    private int energyCost = 80;

    private float abilityCooldownPeriod = 5.0f;

    public override void Init()
    {
        abilityCooldownRemaining = 0;
        isActive = false;
    }

    public override void UserPress()
    {
        if (abilityCooldownRemaining <= 0 && user.Energy >= energyCost)
        {
            user.DrainEnergy(energyCost);
            user.GiveHealth(1);
            abilityCooldownRemaining = abilityCooldownPeriod;
        }
    }

    public override void FixedUpdateCall()
    {
        if (abilityCooldownRemaining > 0)
        {
            abilityCooldownRemaining -= Time.fixedDeltaTime;
        }
    }

    public override void ForceBreak()
    {
        // Nothing happens
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
}
