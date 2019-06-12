using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaddyLogic : NetworkBehaviour
{
    [SerializeField]
    private float Speed = 25f;
    
    private float timeToNextMove = 0.1f;

    MoveController movement;

    void Start()
    {
        movement = new ScaryCuboidMoveController(gameObject, Speed, timeToNextMove);
    }

    void FixedUpdate()
    {
        if (!isServer) return;

        movement.UpdateMove(Time.fixedDeltaTime);
    }
}
