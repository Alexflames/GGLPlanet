using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalObjectsRotation : NetworkBehaviour
{
    ObjectsOnScreen objProvider;

    // Start is called before the first frame update
    void Start()
    {
        objProvider = GetComponentInChildren<ObjectsOnScreen>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            foreach (var obj in objProvider.GetObjectsOnScreen().Values)
            {
                var gameObj = obj as GameObject;
                if (!isObjectToRotate(gameObj)) continue;

                gameObj.transform.eulerAngles = transform.eulerAngles;
            }
        }
    }

    bool isObjectToRotate(GameObject obj)
    {
        if (obj == null) return false;

        return obj.CompareTag("Enemy") || obj.CompareTag("Player");
    }
}
