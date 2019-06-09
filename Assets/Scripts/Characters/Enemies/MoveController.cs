using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{
    protected GameObject character;
    protected float speed;

    public MoveController(GameObject character, float speed)
    {
        this.character = character;
        this.speed = speed;
    }

    public virtual void UpdateMove(float deltaTime)
    {
        character.transform.Translate(NPCMovement.ChaoticMovement(speed * deltaTime));
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
