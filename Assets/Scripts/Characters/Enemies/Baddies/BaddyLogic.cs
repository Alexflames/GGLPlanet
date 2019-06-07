using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaddyLogic : NetworkBehaviour
{
    public float Speed = 25f;
    
    private Vector2 moveDir = new Vector2();
    private float timeToNextMove = 0.1f;

    void FixedUpdate()
    {
        if (!isServer) return;

        if (timeToNextMove <= 0)
        {
            moveDir = NPCMovement.ChaoticMovementAdditive(moveDir, Speed);
            timeToNextMove = Random.Range(0.1f, 0.35f);
        }
        timeToNextMove -= Time.fixedDeltaTime;

        transform.Translate(moveDir);
    }
}
