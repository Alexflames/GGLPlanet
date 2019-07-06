using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispLogic : MonoBehaviour
{

    private Vector2 direction = Vector2.zero;
    private float Speed = 1;

    public void SetSpeed(float speedToSet)
    {
        Speed = speedToSet;
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * Speed);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (gameObject == null) return;

        if (coll.name.ToLower() == "heart")
        {
            StatsManager stats = coll.transform.parent.gameObject.GetComponent<StatsManager>();
            AttackInformation attack = new AttackInformation(gameObject, 1);
            stats.DealDamage(attack);
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }

}
