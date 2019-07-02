using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KamalistAttack : CuboidAttack
{
    [SerializeField]
    private GameObject grenadePrefab = null;

    [SerializeField]
    private Color attColor = Color.green;
    public override Color attackColor
    {
        get
        {
            return attColor;
        }
    }

    [SerializeField]
    private float attackDuration = 5f;
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
            return 2000;
        }
    }


    public override void AttStart()
    {
        float angle = Random.Range(0, 360);
        for (int i = 0; i < 4; i++) {
             GameObject g = GameObject.Instantiate(grenadePrefab, transform.position, Quaternion.Euler(0, 0, angle));
             angle += 90;
             NetworkServer.Spawn(g);
        }
    }

    public override void AttUpdate(float attackTimeLeft)
    {
        
    }

    // TODO: CAN BE UPGRADED TO EITHER LETTING BULLETS CONTINUE FLYING
    // OR SLOWLY MAKE THEM FADE OUT
    public override void AttEnd() {
    }
}
