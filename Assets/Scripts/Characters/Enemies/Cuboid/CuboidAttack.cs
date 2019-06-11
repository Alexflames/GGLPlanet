using UnityEngine;
using Mirror;

public abstract class CuboidAttack : NetworkBehaviour, Attack {
    public abstract void AttStart();
    public abstract void AttUpdate(float attackTimeLeft);
    public abstract void AttEnd();
    public abstract float duration {get;}
    public abstract int priority {get;}
    public abstract Color attackColor {get; }
}
