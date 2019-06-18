using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/// <summary>
/// So here is an explanation of how this script works. The bullet spawning is done at server, 
/// and on spawn each client assigns the bullet without any need for server to make it instead of clients. 
/// 
/// When a new client is connected, the bullets only appear and activate when the scene is loaded for the client. 
/// And the client will sync variables of bullet with server and add it to the bullet updating list
/// through SetBullet() call in Start()
/// 
/// Burst manager updates the position of bullets by calling its UpdateProjectile() function
/// </summary>
public class ExtremeBaddyProj : NetworkBehaviour
{
    [SyncVar]
    public GameObject owner;
    [SyncVar]
    public int index;
    [SyncVar]
    public int damage = 1;

    public int speed = 20;
    
    void Start()
    {
        // TODO: TEMPORARY, CHANGE ON MASTER, EITHER DUPLICATE SCRIPTS OR DELETE EBB
        var burst = owner.GetComponent<BurstAttack>();
        if (burst)
        {
            owner.GetComponent<BurstAttack>().SetBullet(this, index);
        }
        else
        {
            owner.GetComponent<ExtremeBaddyBurst>().SetBullet(this, index);
        }
        
    }

    public void UpdateProjectile()
    {
        transform.Translate(Vector2.right * 0.001f * speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isServer) return;

        if (coll.name == gameObject.name) return;

        if (coll.name.ToLower() == "heart")
        {
            var stats = coll.transform.parent.gameObject.GetComponent<StatsManager>();
            AttackInformation attack = new AttackInformation(owner, damage);
            stats.DealDamage(attack);
            NetworkServer.Destroy(gameObject);
        }

        if (coll.gameObject.tag == "Environment") {
            NetworkServer.Destroy(gameObject);
        }
    }
}
