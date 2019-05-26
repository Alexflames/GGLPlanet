using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ExtremeBaddyProj : NetworkBehaviour
{
    public void UpdateProjectile()
    {
        transform.Translate(Vector2.right * 0.02f);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!isServer) return;

        if (coll.name == "Heart")
        {
            Destroy(coll.transform.parent.gameObject);
            //foreach (var buddy in GameObject.FindGameObjectsWithTag("Enemy"))
            //{
            //    buddy.GetComponent<BaddyLogic>().HP = 6;
            //}
        }
    }
}
