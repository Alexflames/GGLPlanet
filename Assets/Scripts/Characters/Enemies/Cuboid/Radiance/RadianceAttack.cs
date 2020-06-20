using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadianceAttack : CuboidAttack
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
    private GameObject zone;
    private GameObject player;

    public void CreateZone(Vector3 dirOfCube)
    {
        var zoneInstance = GameObject.Instantiate(zone,
            dirOfCube,
            Quaternion.identity);
        var zoneLogic = zoneInstance.GetComponent<ZoneLogic>();
        zoneLogic.SetDirection(dirOfCube);
        Destroy(zoneInstance, 2);
    }

    public override void AttStart()
    {
        var player = GameObject.FindGameObjectsWithTag("Player");
        if (player == null) return;
        var dirOfCube = gameObject.transform.position;
        CreateZone(dirOfCube);
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
