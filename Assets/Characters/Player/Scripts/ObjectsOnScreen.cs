using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsOnScreen : MonoBehaviour
{
    public Hashtable objectsOnScreen;
    public int counter = 0;
    void OnTriggerEnter2D(Collider2D coll)
    {
        counter++;
        objectsOnScreen[coll.gameObject.GetHashCode()] = coll.gameObject;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        objectsOnScreen.Remove(coll.GetInstanceID());
    }

    // Start is called before the first frame update
    void Start()
    {
        objectsOnScreen = new Hashtable();
    }

    public Hashtable GetObjectsOnScreen()
    {
        return objectsOnScreen;
    }
    
}
