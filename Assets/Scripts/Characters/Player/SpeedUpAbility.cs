using UnityEngine;

public class SpeedUpAbility : PlayerAbility
{
    public override void Activate()
    {
        isActive = true;
        abilityDurationRemaining = abilityDuration;
    }

    public override void Deactivate()
    {
        
    }

    public override void ForceBreak()
    {

    }

    public override bool IsActive
    {
        get { return isActive; }
    }

    public override int EnergyCost
    {
        get { return energyCost; }
    }

    private void Start()
    {
        isActive = false;
        energyCost = 50;
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

    private bool isActive;
    private int energyCost;
    private double abilityDuration;
    private double abilityDurationRemaining;
}
