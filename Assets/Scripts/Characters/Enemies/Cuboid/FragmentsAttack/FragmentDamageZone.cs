using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentDamageZone : MonoBehaviour
{
    List<GameObject> playersHit = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name.ToLower() == "heart" && !playersHit.Contains(collider2D.gameObject))
        {
            playersHit.Add(collider2D.gameObject);
            var attack = new AttackInformation(null, 1);
            collider2D.gameObject.GetComponentInParent<StatsManager>().DealDamage(attack);            
        }
    }
}
