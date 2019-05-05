using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerWorldInit : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.2f, 0.8f);
        }   
    }
}
