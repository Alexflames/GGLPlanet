using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddyLaserAttack : BaddyAttack
{
    [SerializeField]
    private GameObject BaddyLaserPrefab;
    private GameObject BaddyLaser;


    private GameObject Player;
    List<GameObject> damagedPlayers;
    private CollidingPlayers collidingPlayers;

    private SpriteRenderer LaserSprite;

    private BaddyAttackManager Bmgr;

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
            return 2;
        }
    }

    private void Start()
    {
        BaddyLaser = Instantiate(BaddyLaserPrefab, gameObject.transform.position, Quaternion.identity);
        LaserSprite = BaddyLaser.GetComponent<SpriteRenderer>();
        collidingPlayers = BaddyLaser.GetComponent<CollidingPlayers>();
        BaddyLaser.SetActive(false);
        Bmgr = gameObject.GetComponent<BaddyAttackManager>();
    }

    public override void AttStart()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        var pos = gameObject.transform.position;
        if (Player.transform.position.x - gameObject.transform.position.x > 0)
            pos.x += 1.3f;
        else
            pos.x -= 1.3f;
        BaddyLaser.transform.position = pos;

        Vector3 selfEulerAngles = BaddyLaser.transform.rotation.eulerAngles;
        var offset = Player.transform.position - BaddyLaser.transform.position;
        var angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
        angle += selfEulerAngles.z;
        BaddyLaser.transform.rotation = Quaternion.Euler(0, 0, angle);
        BaddyLaser.SetActive(true);
        var color = LaserSprite.color;
        color.a = 0;
        LaserSprite.color = color;
        
    }

    public override void AttUpdate(float attackTimeLeft)
    {
        //if (BaddyLaser) { 
        //Vector3 selfEulerAngles = BaddyLaser.transform.rotation.eulerAngles;
        //var offset = Player.transform.position - BaddyLaser.transform.position;
        //var angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
        //angle += selfEulerAngles.z;
        //BaddyLaser.transform.rotation = Quaternion.Euler(0, 0, angle);
        //}

        if (attackTimeLeft < attackDuration * 0.6)
        {
            
            var playersToHit = collidingPlayers.GetCollidingPlayers();

            foreach (GameObject player in playersToHit)
            {
                if (!damagedPlayers.Contains(player))
                {
                    damagedPlayers.Add(player);
                    Bmgr.InjurePlayer(player, 1);
                }
            }
            if(attackTimeLeft == attackDuration * 0.4f)
            {
                damagedPlayers.Clear();
            }
        }
        else
        {
            LaserSprite.color = new Color(LaserSprite.color.r, LaserSprite.color.g, LaserSprite.color.b, Mathf.Lerp(0, 1, (attackDuration - attackTimeLeft) / (attackDuration * 0.4f)));
        }
    }

    public override void AttEnd()
    {
        if(BaddyLaser != null)
        {
            BaddyLaser.SetActive(false);
        }
    }
}
