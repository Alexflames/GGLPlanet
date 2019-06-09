using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LockCameraOnPlayer : NetworkBehaviour
{
    new GameObject camera;
    private Quaternion savedRotation;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        camera = GameObject.Find("Main Camera");
        camera.transform.parent = gameObject.transform;
        camera.transform.position = new Vector3(
                gameObject.transform.position.x, 
                gameObject.transform.position.y, 
                camera.transform.position.z);
        savedRotation = camera.transform.rotation;
    }

    void OnDestroy()
    {
        if (isLocalPlayer)
        {
            camera.transform.parent = null;
            camera.transform.rotation = savedRotation;
            camera.GetComponent<Camera>().enabled = true;
        }
    }
}
