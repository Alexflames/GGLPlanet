using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LockCameraOnPlayer : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GameObject.Find("Main Camera").transform.parent = gameObject.transform;    
    }
}
