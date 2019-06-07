using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    


    // Start is called before the first frame update
    void Start()
    {
        
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

    void OnDestroy()
    {
        //AttEnd();
    }
}
