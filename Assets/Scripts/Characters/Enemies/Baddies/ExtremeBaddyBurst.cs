using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ExtremeBaddyBurst : NetworkBehaviour
{
    private float timeToNext = 0.25f;
    private float TTNLeft;

    public GameObject bullet;

    [SerializeField]
    private int averageBulletCount = 5;
    [SerializeField]
    private int bulletLifeTime = 4;
    [SerializeField]
    private ExtremeBaddyProj[] bullets;
    int bulletArrayIndex = 0;
    private int arrCapacity = 0;

    // Start is called before the first frame update
    void Start()
    {
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
    void FixedUpdate()
    {
        if (isServer)
        {
            UpdateBullets();
            TTNLeft -= Time.deltaTime;
            
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
        GameObject new_bullet = GameObject.Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
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

    public void IncreaseAverageBullet(int value)
    {
        averageBulletCount += value;
        
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
