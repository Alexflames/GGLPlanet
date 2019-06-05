using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalPlayerSetup : NetworkBehaviour
{
    public List<GameObject> toSetActive;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            foreach (var obj in toSetActive)
            {
                obj.SetActive(true);
            }
        }
    }
    
}
