using UnityEngine;

public abstract class PlayerAbility
{
    // Called when the ability is initialized by the ability manager
    public abstract void Init();

    public void PassTarget(StatsManager _target)
    {
        target = _target;
    }

    // Called when the ability is activated by player
    public abstract void Activate();

    // Called when the ability is deactivated by player, if it's possible
    public abstract void Deactivate();

    // Called by an ability manager every fixed update call
    public abstract void FixedUpdateCall();

    // Called when the player's ability is interrupted (e. g. the player dies)
    public abstract void ForceBreak();

    public abstract bool IsActive { get; }

    public abstract float CooldownRemaining { get; }

    public abstract int EnergyCost { get; }

    protected StatsManager target = null;
}
