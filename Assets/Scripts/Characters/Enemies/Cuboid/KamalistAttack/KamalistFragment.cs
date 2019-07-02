using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KamalistFragment : NetworkBehaviour
{
    private Vector2 f;
    [SerializeField]
    private float linearSpeedFactor = 0.040f;
    private float rotationSpeed = 8;
    private int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        f = Vector2.right;
    }

     public static Vector2 Rotate(Vector2 v, float degrees) {
         float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
         float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
         float tx = v.x;
         float ty = v.y;
         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);
         return v;
     }

    void FixedUpdate()
    {
        transform.Translate(f * linearSpeedFactor);
        transform.Rotate (Vector3.forward * -(rotationSpeed));
        f = Rotate(f, rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isServer) return;
        if (coll.name == gameObject.name) return;

        if (coll.name.ToLower() == "heart")
        {
            var stats = coll.transform.parent.gameObject.GetComponent<StatsManager>();
            AttackInformation attack = new AttackInformation(null, damage);
            stats.DealDamage(attack);
            NetworkServer.Destroy(gameObject);
        }

        if (coll.gameObject.tag == "Environment") {
            NetworkServer.Destroy(gameObject);
        }
    }
}
