using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public  class AttackManager : NetworkBehaviour
{
    public virtual void UpdateAttack()
    {
    }
    protected virtual Attack ChooseAttack()
    {
        return null;
    }
    public virtual bool IsAttacking()
    {
        return false;
    }


}
