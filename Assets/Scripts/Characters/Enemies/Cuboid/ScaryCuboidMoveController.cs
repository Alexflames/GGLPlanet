using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryCuboidMoveController : MoveController
{
    private float timeToNextMove;
    public ScaryCuboidMoveController(GameObject character, float speed, float timeToFirstMove) : base(character, speed)
    {
        timeToNextMove = timeToFirstMove;
    }

    Vector2 moveDir = new Vector2();
    public override void UpdateMove(float deltaTime)
    {
        if (timeToNextMove <= 0)
        {
            moveDir = NPCMovement.ChaoticMovementAdditive(moveDir, 0.5f, speed);
            timeToNextMove = Random.Range(0.1f, 0.35f);
        }
        timeToNextMove -= deltaTime;

        character.transform.Translate(moveDir * deltaTime);
    }
}
