using UnityEngine;

public class SpeedUpAbility : PlayerAbility
{
    [SerializeField]
    private int energyCost = 50;

    [SerializeField]
    private float abilityDuration = 5;

    public override void Init()
    {
        isActive = false;
    }

    public override void Activate()
    {
        isActive = true;
        abilityDurationRemaining = abilityDuration;
    }

    public override void Deactivate()
    {
        // This is left empty because the player won't be able to deactivate
        // the ability manually
    }

    public override void UpdateStats(PlayerStatMod statMod)
    {
        if (isActive)
        {
            statMod.SpeedMul *= 1.5f;
        }
    }

    public override void FixedUpdateCall()
    {
        if (abilityDurationRemaining > 0)
        {
            abilityDurationRemaining -= Time.fixedDeltaTime;
        }
        else if (isActive)
        {
            isActive = false;
        }
    }

    public override void ForceBreak()
    {
        isActive = false;
    }

    public override bool IsActive
    {
        get { return isActive; }
    }

    public override int EnergyCost
    {
        get { return energyCost; }
    }

    private bool isActive = false;
    private float abilityDurationRemaining = 0;
}
