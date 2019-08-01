using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public abstract class BaddyAttack : NetworkBehaviour,Attack
{
    public abstract void AttStart();
    public abstract void AttUpdate(float attackTimeLeft);
    public abstract void AttEnd();
    public abstract float duration { get; }
    public abstract int priority { get; }
}
