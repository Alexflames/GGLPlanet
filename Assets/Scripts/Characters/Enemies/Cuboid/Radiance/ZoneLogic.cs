using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLogic : MonoBehaviour
{

    private Vector2 direction = Vector2.zero;

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (gameObject == null) return;

        if (coll.name.ToLower() == "heart")
        {
            StatsManager stats = coll.transform.parent.gameObject.GetComponent<StatsManager>();
            AttackInformation attack = new AttackInformation(gameObject, 3);
            stats.DealDamage(attack);
            Destroy(gameObject);
        }
    }

}
