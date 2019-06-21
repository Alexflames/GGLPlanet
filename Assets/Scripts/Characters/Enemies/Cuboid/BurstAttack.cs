using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BurstAttack : CuboidAttack
{
    private float timeToNext = 0.25f;
    private float TTNLeft;

    [SerializeField]
    private GameObject bulletPrefab = null;

    [SerializeField]
    private int averageBulletCount = 5;
    [SerializeField]
    private int bulletLifeTime = 4;
    [SerializeField]
    private ExtremeBaddyProj[] bullets;
    private int bulletArrayIndex = 0;
    private int arrCapacity = 0;

    // Movement during attack
    [SerializeField]
    private float moveSpeedDuringAttack = 20;
    private ScaryCuboidMoveController moveCtrl;

    // Cuboid-attack-specific properties
    [SerializeField]
    private Color attColor = Color.yellow;
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
            return 2;
        }
    }

    // Start is called before the first frame update
    public override void AttStart()
    {
        if (isServer)
        {
            moveCtrl = new ScaryCuboidMoveController(gameObject, moveSpeedDuringAttack, 0.1f);
        }
        // If we have 20 bullets per tick, then we have 20 / timeToNext bullets per second
        // And we want to have bullets for at least bulletLifeTime seconds
        arrCapacity = System.Convert.ToInt16(averageBulletCount / timeToNext) * bulletLifeTime;
        bullets = new ExtremeBaddyProj[arrCapacity];
        TTNLeft = timeToNext;
    }

    void UpdateBullets()
    {
        foreach (var bullet in bullets)
        {
            if (bullet != null)
            {
                bullet.UpdateProjectile();
            }
        }
    }

    // Update is called once per frame
    public override void AttUpdate(float attackTimeLeft)
    {
        if (isServer)
        {
            moveCtrl.UpdateMove(Time.fixedDeltaTime);
            UpdateBullets();
            TTNLeft -= Time.fixedDeltaTime;

            if (TTNLeft < 0)
            {
                TTNLeft = timeToNext;
                int numberOfBullets = Random.Range(averageBulletCount - 1, averageBulletCount + 2);

                for (int i = 0; i < numberOfBullets; i++)
                {
                    var oldBullet = bullets[bulletArrayIndex];
                    if (oldBullet != null)
                    {
                        NetworkServer.Destroy(oldBullet.gameObject);
                    }
                    InitializeBullet();
                }
            }
        }
        else
        {
            UpdateBullets();
        }
    }

    // Bullets set themselves in cyclic format, when they reach the end of array, they
    // return to its start, changing the oldest bullets
    private void InitializeBullet()
    {
        GameObject new_bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        var newBulletComp = new_bullet.GetComponent<ExtremeBaddyProj>();
        newBulletComp.index = bulletArrayIndex;
        newBulletComp.owner = gameObject;
        newBulletComp.damage = 1;
        NetworkServer.Spawn(new_bullet);

        bulletArrayIndex = (bulletArrayIndex + 1) % arrCapacity;
    }

    public void SetBullet(ExtremeBaddyProj bulletScr, int index)
    {
        bullets[index] = bulletScr;
    }

    public void IncreaseAverageBulletCount(int value)
    {
        averageBulletCount += value;
    }

    // TODO: CAN BE UPGRADED TO EITHER LETTING BULLETS CONTINUE FLYING
    // OR SLOWLY MAKE THEM FADE OUT
    public override void AttEnd() {
        if (isServer)
        {
            OnDestroy();
        }
        bulletArrayIndex = 0;
    }

    void OnDestroy()
    {
        for (int i = 0; i < arrCapacity; i++)
        {
            if (bullets[i] != null)
            {
                NetworkServer.Destroy(bullets[i].gameObject);
            }
        }
    }
}
