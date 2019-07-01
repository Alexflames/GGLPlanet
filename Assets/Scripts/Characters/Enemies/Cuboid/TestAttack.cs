using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : CuboidAttack
{
    public override int priority
    {
        get
        {
            return 10;
        }
    }

    [SerializeField]
    private float AttackDuration;
    public override float duration
    {
        get
        {
            return AttackDuration;
        }
    }

    [SerializeField]
    private Color AttackColor;
    public override Color attackColor
    {
        get
        {
            return AttackColor;
        }
    }

    public override void AttStart()
    {
        print("Attack started!");
    }

    public override void AttUpdate(float attackTimeLeft)
    {
        print("Attack updated!");
    }

    public override void AttEnd()
    {
        print("Attack ended!");
    }
}
