using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispAttack : CuboidAttack
{

    [SerializeField]
    public float attackDuration;

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

    public float timeleft;

    [SerializeField]
    private int WispNumber = 30;

    [SerializeField]
    private float WispSpeed = 1.4f;
    public void CreateWisp(Vector3 dirOfCube, float attackTimeLeft)
    {
        timeleft = attackTimeLeft;
        var wispInstance = GameObject.Instantiate(wisp,
            dirOfCube,
            Quaternion.Euler(0, 0, Random.Range(0, 360)));
        var wispLogic = wispInstance.GetComponent<WispLogic>();
        wispLogic.SetDirection(dirOfCube);
        wispLogic.SetSpeed(WispSpeed);
        wispLogic.SetTimeLeft(timeleft);
        Destroy(wispInstance, 4);
    }
    public override void AttStart()
    {
        float attackTimeLeft = attackDuration;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        var dirOfCube = gameObject.transform.position;
        for (int i = 0; i < WispNumber; i++)
        {
            CreateWisp(dirOfCube, attackTimeLeft);
        }
       
    }

    public override void AttUpdate(float timeleft)
    {
        if (player == null) return;
        timeleft -= Time.deltaTime;
            print("Attack updated: time left ");
    }

    public override void AttEnd()
    {
        if (player == null) return;
        print("Attack ended");
    }

}
