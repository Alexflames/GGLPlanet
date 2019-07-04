using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcTargetAttack : CuboidAttack
{
    public override int priority
    {
        get
        {
            return 1;
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

    [SerializeField]
    private float SphereSpeed = 2;

    [SerializeField]
    private GameObject arcSphere;
    private GameObject player; // Player in scene

    [SerializeField]
    private float ArcAngle = 45;
    [SerializeField]
    private int SphereNumber = 3;

    public void CreateSphere(Vector3 dirToPlayer)
    {
        var arcSphereInstance = GameObject.Instantiate(arcSphere,
            gameObject.transform.position + dirToPlayer,
            Quaternion.identity);

        var arcSphereLogic = arcSphereInstance.GetComponent<ArcSphereLogic>();
        arcSphereLogic.SetDirection(dirToPlayer);
        arcSphereLogic.SetSpeed(SphereSpeed);

        Destroy(arcSphereInstance, 2);
    }

    public override void AttStart()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        var dirToPlayer = Vector3.Normalize(player.transform.position - gameObject.transform.position);

        for (int i = 0; i < SphereNumber; i++)
        {
            var angle = Mathf.Lerp(-ArcAngle / 2, ArcAngle / 2, i / (SphereNumber - 1.0f));
            var rotation = Quaternion.Euler(0, 0, angle);
            var newDirToPlayer = rotation * dirToPlayer;
            CreateSphere(newDirToPlayer);
        }

    }

    public override void AttUpdate(float attackTimeLeft)
    {
        if (player == null) return;

        print("Attack updated!");
    }

    public override void AttEnd()
    {
        if (player == null) return;

        print("Attack ended!");
    }
}