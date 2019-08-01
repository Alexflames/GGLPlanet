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
    private CollidingEnvironment collidingEnvironment;

    private SpriteRenderer LaserSprite;

    private BaddyAttackManager Bmgr;
    private Transform scale;

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
        LaserSprite = BaddyLaser.GetComponentInChildren<SpriteRenderer>();
        collidingPlayers = BaddyLaser.GetComponent<CollidingPlayers>();
        collidingEnvironment = BaddyLaser.GetComponent<CollidingEnvironment>();
        BaddyLaser.SetActive(false);
        Bmgr = gameObject.GetComponent<BaddyAttackManager>();
    }

    public override void AttStart()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        var pos = gameObject.transform.position;
        if (Player.transform.position.x - gameObject.transform.position.x > 0)
            pos.x += 0.5f;
        else
            pos.x -= 0.5f;
        BaddyLaser.transform.position = pos;

        BaddyLaser.SetActive(true);
        var color = LaserSprite.color;
        color.a = 0;
        LaserSprite.color = color;
        damagedPlayers = new List<GameObject>();

    }

    public override void AttUpdate(float attackTimeLeft)
    {
        if (BaddyLaser && Environment())
        {
            var offset = Player.transform.position - BaddyLaser.transform.position;
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            BaddyLaser.transform.rotation = Quaternion.Euler(0, 0, angle+180);
        }

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
            if(attackTimeLeft < attackDuration * 0.4f)
            {
                damagedPlayers.Clear();
            }
        }
        else
        {
            LaserSprite.color = new Color(LaserSprite.color.r, LaserSprite.color.g, LaserSprite.color.b, Mathf.Lerp(0, 1, (attackDuration - attackTimeLeft) / (attackDuration * 0.4f)));
            if(Environment())
            BaddyLaser.transform.localScale = new Vector3(Mathf.Lerp(1f, 1.8f, (attackDuration - attackTimeLeft) / (attackDuration * 0.4f)), 1f, 1f);
        }
    }

    public override void AttEnd()
    {
        if(BaddyLaser != null)
        {
            BaddyLaser.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            BaddyLaser.SetActive(false);
        }
    }

    private bool Environment()
    {
        var WallsToStop = collidingEnvironment.GetCollidingWalls();
        if (WallsToStop.Count == 0)
            return true;
        else
            return false;
    }
}
