using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaddyLogic : NetworkBehaviour
{
    public float Speed = 25f;
    
    private Vector2 moveDir = new Vector2();
    private float timeToNextMove = 0.1f;

    MoveController movement;

    void Start()
    {
        movement = new ScaryCuboidMoveController(gameObject, Speed, 0.1f);
    }

    void FixedUpdate()
    {
        if (!isServer) return;

        movement.UpdateMove(Time.fixedDeltaTime);
    }
}
