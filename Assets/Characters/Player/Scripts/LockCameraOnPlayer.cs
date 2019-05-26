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
        var camera = GameObject.Find("Main Camera");
        camera.transform.parent = gameObject.transform;
        camera.transform.position = new Vector3(
                gameObject.transform.position.x, 
                gameObject.transform.position.y + 1, 
                camera.transform.position.z);
    }
}
