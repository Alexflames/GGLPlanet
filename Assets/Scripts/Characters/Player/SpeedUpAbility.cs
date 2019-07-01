using UnityEngine;

public class SpeedUpAbility : PlayerAbility
{
    private int energyCost = 50;

    private float abilityDuration = 5;

    private float speedMul = 1.2F;

    public override void Init()
    {
        isActive = false;
    }

    public override void Activate()
    {
        TurnOn();
    }

    public override void Deactivate()
    {
        // This is left empty because the player won't be able to deactivate
        // the ability manually
    }

    public override void FixedUpdateCall()
    {
        if (abilityDurationRemaining > 0)
        {
            abilityDurationRemaining -= Time.fixedDeltaTime;
        }
        else if (isActive)
        {
            TurnOff();
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

    public override int EnergyCost
    {
        get { return energyCost; }
    }

    private bool isActive = false;
    private float abilityDurationRemaining = 0;

    private void TurnOn()
    {
        isActive = true;
        target.SpeedAddMul(speedMul);
        abilityDurationRemaining = abilityDuration; 
    }

    private void TurnOff()
    {
        isActive = false;
        target.SpeedAddMul(1 / speedMul);
    }
}
