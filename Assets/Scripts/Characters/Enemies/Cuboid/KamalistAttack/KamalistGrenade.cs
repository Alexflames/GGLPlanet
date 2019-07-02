using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class KamalistGrenade : NetworkBehaviour
{
    private Vector2 f;
    private float linearSpeedFactor = 0.023f;
    private float rotationSpeed = 4;
    private float timer = 0;
    private float explodeTime = 3;
    private int damage = 5;

    [SerializeField]
    private GameObject fragmentPrefab;
    // Start is called before the first frame update
    void Start()
    {
        f = Vector2.up;
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
        if (timer >= explodeTime) {
            Explosion ();
            NetworkServer.Destroy(gameObject);
        }
        transform.Translate(f * linearSpeedFactor);
        transform.Rotate (Vector3.forward * -(rotationSpeed));
        f = Rotate(f, rotationSpeed);
        timer += Time.fixedDeltaTime;
    }
  
    void Explosion () {
        float angle = Random.Range(0, 360);
        for (int i = 0; i < 8; i++) {
             GameObject g = GameObject.Instantiate(fragmentPrefab, transform.position, Quaternion.Euler(0, 0, angle));
             angle += 45;
             NetworkServer.Spawn(g);
        }
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
            Explosion ();
            NetworkServer.Destroy(gameObject);
        }
    }
}
