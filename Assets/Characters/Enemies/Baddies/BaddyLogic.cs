using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaddyLogic : NetworkBehaviour
{
    public float Speed = 0.005f;

    public Vector2 moveDir;
    private float timeToNextMove = 0.1f;

    void Update()
    {
        if (!isServer) return;

        if (timeToNextMove <= 0)
        {
            moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            moveDir.Normalize();
            timeToNextMove = Random.Range(0.1f, 0.35f);
        }

        timeToNextMove -= Time.deltaTime;
        
        transform.Translate(moveDir * Speed);
    }
}
