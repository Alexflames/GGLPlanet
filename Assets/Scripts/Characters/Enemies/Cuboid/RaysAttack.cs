using UnityEngine;
using Mirror;

// Suppressing fire ~ Spiral of doom
public class RaysAttack : CuboidAttack
{
    [SerializeField]
    private float attackDuration = 8f;

    [SerializeField]
    private float afterAttackTime = 2f;

    [SerializeField]
    private float turningSpeed = 700f;

    [SerializeField]
    private GameObject bulletPrefab = null;

    [SerializeField]
    private Color attColor = Color.green;

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
    
    public override Color attackColor
    {
        get
        {
            return attColor;
        }
    }

    public override void AttStart()
    {
        if (isServer)
        {
            moveCtrl = new ScaryCuboidMoveController(gameObject, moveSpeedDuringAttack, 1.0f);
        }
        bulletArrayCapacity = System.Convert.ToInt32((attackDuration - afterAttackTime) * attackDirectionsAmount / TTN);
        bulletArray = new ExtremeBaddyProj[bulletArrayCapacity];

        attackAngle = Random.Range(0f, 360f);
    }

    public override void AttUpdate(float attackTimeLeft)
    {
        if (isServer)
        {
            float dt = Time.fixedDeltaTime;

            float attackTimePassed = attackDuration - attackTimeLeft;
            float spinningSpeed = (attackTimePassed / attackDuration) * 3f - 1f;
            attackAngle += turningSpeed * dt * spinningSpeed;
            attackTimeLeft -= dt;

            moveCtrl.UpdateMove(dt);
            UpdateBullets();
            TTNLeft -= dt;

            if (TTNLeft < 0 && attackTimeLeft > afterAttackTime)
            {
                if (spinningSpeed > 1)
                {
                    TTNLeft = TTN / spinningSpeed;
                }
                else
                {
                    TTNLeft = TTN;
                }
                for (int i = 0; i < attackDirectionsAmount; i++)
                {
                    SpawnBullet(attackAngle + i * (360f / attackDirectionsAmount));
                }
            }
        }
        else
        {
            UpdateBullets();
        }
    }

    public override void AttEnd()
    {
        if (isServer)
        {
            OnDestroy();
        }
        bulletArrayIndex = 0;
    }

    void OnDestroy()
    {
        for (int i = 0; i < bulletArrayCapacity; i++)
        {
            if (bulletArray[i] != null)
            {
                NetworkServer.Destroy(bulletArray[i].gameObject);
            }
        }
    }

    private void SpawnBullet(float angle)
    {
        GameObject new_bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        
        var newBulletComp = new_bullet.GetComponent<ExtremeBaddyProj>();
        newBulletComp.index = bulletArrayIndex;
        newBulletComp.owner = gameObject;
        newBulletComp.damage = 1;

        var oldBullet = bulletArray[bulletArrayIndex];
        if (oldBullet != null)
        {
            NetworkServer.Destroy(oldBullet.gameObject);
        }
        bulletArray[bulletArrayIndex] = newBulletComp;
        NetworkServer.Spawn(new_bullet);

        bulletArrayIndex = (bulletArrayIndex + 1) % bulletArrayCapacity;
    }

    void UpdateBullets()
    {
        foreach (var bullet in bulletArray)
        {
            if (bullet != null)
            {
                bullet.UpdateProjectile();
            }
        }
    }
    
    private ScaryCuboidMoveController moveCtrl;
    private ExtremeBaddyProj[] bulletArray;
    private int bulletArrayIndex = 0;
    private int bulletArrayCapacity = 0;
    private float moveSpeedDuringAttack = 5f;
    private float attackAngle = 0;
    private int attackDirectionsAmount = 8;
    private float TTN = 1f / 10;
    private float TTNLeft = 0f;
}
