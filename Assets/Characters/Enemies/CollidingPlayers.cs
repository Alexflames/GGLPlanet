using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingPlayers : MonoBehaviour
{
    public List<GameObject> colliding = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D coll) {
        print("enter");
        if (coll.name.ToLower() == "heart")
        {
            colliding.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.ToLower() == "heart")
        {
            colliding.Remove(coll.gameObject);
        }
    }

    public List<GameObject> GetCollidingPlayers()
    {
        return colliding;
    }
}
