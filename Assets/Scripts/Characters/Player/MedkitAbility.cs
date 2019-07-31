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

    public override void Activate()
    {
        if (abilityCooldownRemaining <= 0)
        {
            target.GiveHealth(1);
            abilityCooldownRemaining = abilityCooldownPeriod;
        }
    }

    public override void Deactivate()
    {
        // This is left empty because the player won't be able to deactivate
        // the ability manually
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
