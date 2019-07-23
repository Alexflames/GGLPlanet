using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalObjectsRotation : NetworkBehaviour
{
    ObjectsOnScreen objProvider;
    [SerializeField]
    private Vector3 EulerAnglesOffset = Vector3.zero;

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

                gameObj.transform.eulerAngles = transform.eulerAngles + EulerAnglesOffset;
            }
        }
    }

    bool isObjectToRotate(GameObject obj)
    {
        return obj != null;
    }
}
