using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProjectileLife : NetworkBehaviour
{
    public float lifeSpan = 1;
    public float Speed = 0.05f;

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeSpan -= Time.deltaTime;
        transform.Translate(Vector2.right * Speed);
        if (lifeSpan < 0)
        {
            NetworkServer.Destroy(gameObject);
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<BaddyLogic>().TakeDamage();
        }
    }
}
