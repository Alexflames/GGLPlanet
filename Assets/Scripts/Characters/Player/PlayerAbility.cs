using Mirror;

public abstract class PlayerAbility
{
    // Called when the ability is activated
    public abstract void Activate();

    // Called when the ability is deactivated by player, if it's possible
    public abstract void Deactivate();

    // 
    public abstract void FixedUpdateCall();

    // Called when the player's ability is interrupted (e. g. the player dies)
    public abstract void ForceBreak();

    public abstract bool IsActive { get; }

    public abstract int EnergyCost { get; }
}
