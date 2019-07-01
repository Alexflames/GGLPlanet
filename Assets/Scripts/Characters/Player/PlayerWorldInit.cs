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
            GetComponent<SpriteRenderer>().color = new Color(
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f));
            GetComponent<Collider2D>().isTrigger = true;
        }
        
    }
}
