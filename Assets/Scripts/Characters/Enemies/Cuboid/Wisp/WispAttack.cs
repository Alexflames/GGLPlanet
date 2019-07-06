using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispAttack : CuboidAttack
{

    [SerializeField]
    private float attackDuration;

    public override float duration
    {
        get
        {
            return attackDuration;
        }
    }

    public override int priority
    {
        get
        {
            return 1;
        }
    }

    [SerializeField]
    private Color attColor;

    public override Color attackColor
    {
        get
        {
            return attColor;
        }
    }

    [SerializeField]
    private GameObject wisp;
    private GameObject player;

    

    [SerializeField]
    private int WispNumber = 30;

    [SerializeField]
    private float WispSpeed = 1.6f;

    public void CreateWisp(Vector3 dirOfCube)
    {
        var wispInstance = GameObject.Instantiate(wisp,
            dirOfCube,
            Quaternion.Euler(0, 0, Random.Range(0, 360)));
        var wispLogic = wispInstance.GetComponent<WispLogic>();
        wispLogic.SetDirection(dirOfCube);
        wispLogic.SetSpeed(WispSpeed);
        Destroy(wispInstance, 2);
    }
    public override void AttStart()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        var dirOfCube = gameObject.transform.position;
        for (int i = 0; i < WispNumber; i++)
        {
            CreateWisp(dirOfCube);
        }
       
    }

    public override void AttUpdate(float attackTimeLeft)
    {
        if (player == null) return;
        print("Attack updated: time left " + attackTimeLeft);
    }

    public override void AttEnd()
    {
        if (player == null) return;
        print("Attack ended");
    }

}
