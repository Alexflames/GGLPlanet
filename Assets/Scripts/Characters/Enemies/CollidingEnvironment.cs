using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingEnvironment : MonoBehaviour
{
    public List<GameObject> colliding = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.ToLower() == "environment")
        {
            colliding.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag.ToLower() == "environment")
        {
            colliding.Remove(coll.gameObject);
        }
    }

    public List<GameObject> GetCollidingWalls()
    {
        return colliding;
    }
}
