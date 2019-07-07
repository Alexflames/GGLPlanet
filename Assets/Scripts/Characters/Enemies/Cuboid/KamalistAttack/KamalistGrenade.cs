using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class KamalistGrenade : NetworkBehaviour
{
    private Vector2 f;
    [SerializeField]
    private float linearSpeedFactor = 0.033f;
    [SerializeField]
    private float rotationSpeed = 4;
    private float timer = 0;
    private float explodeTime = 3;
    private int damage = 5;
    [SerializeField]
    private int numberOfFragments = 8;

    private bool activated = false;
    private bool exploded = false;
    private int spawnedFragments = 0;

    [SerializeField]
    private GameObject fragmentPrefab;
    private List<GameObject> fragments = new List<GameObject>();

    IEnumerator CreateFragments () {
        for (int i = 0; i < numberOfFragments; i++) {
             if (exploded) break;
             GameObject g = GameObject.Instantiate(fragmentPrefab, transform.position, Quaternion.Euler(0, 0, 0));
             NetworkServer.Spawn(g);
             g.SetActive (false);
             spawnedFragments++;
             fragments.Add (g);
             yield return new WaitForEndOfFrame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        f = Vector2.up;
        StartCoroutine (CreateFragments ());
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
        if (activated) {
            bool res = Explosion (numberOfFragments);
            if (res) NetworkServer.Destroy(gameObject);
            return;
        }
        transform.Translate(f * linearSpeedFactor);
        transform.Rotate (Vector3.forward * -(rotationSpeed));
        f = Rotate(f, rotationSpeed);
        timer += Time.fixedDeltaTime;
        if (timer >= explodeTime) activated = true;
    }
  
    bool Explosion (int fragCount) {
        if (spawnedFragments < fragCount) return false;
        exploded = true;
        float angle = Random.Range(0, 360);
        for (int i = 0; i < fragCount; i++) {
             GameObject g = fragments[i];
             g.transform.position = gameObject.transform.position;
             g.transform.Rotate (Vector3.forward * angle);
             angle += 360 / fragCount;
             g.SetActive (true);
             g.GetComponent<KamalistFragment>().Act ();
             NetworkServer.Spawn(g);
        }
        return true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isServer) return;
        if (coll.name == gameObject.name) return;
        if (activated) return;

        if (coll.name.ToLower() == "heart")
        {
            var stats = coll.transform.parent.gameObject.GetComponent<StatsManager>();
            AttackInformation attack = new AttackInformation(null, damage);
            stats.DealDamage(attack);
            activated = true;
        }

        if (coll.gameObject.tag == "Environment") {
            activated = true;
        }
    }
}
