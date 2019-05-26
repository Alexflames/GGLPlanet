using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ExtremeBaddyBurst : NetworkBehaviour
{
    private float timeToNext = 0.25f;
    private float TTNLeft;

    public GameObject bullet;

    static int ARR_SIZE = 100;
    public ExtremeBaddyProj[] bullets = new ExtremeBaddyProj[ARR_SIZE];
    int bulletArrayIndex = 0;

    public int averageBulletCount = 5;

    // Start is called before the first frame update
    void Start()
    {
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

    private void InitializeBullet()
    {
        GameObject new_bullet = GameObject.Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        var newBulletComp = new_bullet.GetComponent<ExtremeBaddyProj>();
        newBulletComp.index = bulletArrayIndex;
        newBulletComp.owner = gameObject;
        newBulletComp.damage = 1;
        NetworkServer.Spawn(new_bullet);

        bulletArrayIndex = (bulletArrayIndex + 1) % ARR_SIZE;
    }

    public void SetBullet(ExtremeBaddyProj bulletScr, int index)
    {
        bullets[index] = bulletScr;
    }

    void OnDestroy()
    {
        for (int i = 0; i < ARR_SIZE; i++)
        {
            if (bullets[i] != null)
            {
                NetworkServer.Destroy(bullets[i].gameObject);
            }
        }
    }
}
