using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMoveController : MoveController
{
    private Transform target;
    public FollowMoveController(GameObject character, float speed, Transform target) : base(character, speed)
    {
        this.target = target;
    }

    public FollowMoveController(GameObject character, float speed) : base(character, speed) { }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public override void UpdateMove(float deltaTime)
    {
        character.transform.Translate(NPCMovement.DirectionalMovement(
            character.transform.position,
            target.position, speed) * deltaTime, Space.World);
    }
}
