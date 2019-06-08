using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EmptyAttack: for testing of attack switching. Only prints debug messages
public class EmptyAttack : CuboidAttack
{
    [SerializeField]
    private float attackDuration = 4f;
    public override float duration {
        get {
            return attackDuration;
        }
    }

    public override int priority {
        get {
            return 1;
        }
    }

    [SerializeField]
    private Color attColor = Color.yellow;
    public override Color attackColor {
        get {
            return attColor;
        }
    }

    public override void AttStart()
    {
        print ("Empty attack started");
    }

    public override void AttUpdate(float attackTimeLeft)
    {
        print ("Empty attack updated: time left "+attackTimeLeft);
    }

    public override void AttEnd()
    {
        print ("Empty attack ended");
    }
}
