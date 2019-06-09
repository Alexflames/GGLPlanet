using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NPCMovement
{
    private static readonly float standSpeedMul = 0.05f;

    public static Vector2 ChaoticMovement(float speed)
    {
        var moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        moveDir.Normalize();
        return moveDir * (speed * standSpeedMul);
    }

    public static Vector2 ChaoticMovementAdditive(Vector2 oldMove, float speed)
    {
        var moveDir = oldMove * 0.6f + new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        moveDir.Normalize();
        return moveDir * (speed * standSpeedMul);
    }

    public static Vector2 DirectionalMovement(Vector2 dir, float speed)
    {
        return dir * (speed * standSpeedMul);
    }

    public static Vector2 DirectionalMovement(Vector2 from, Vector2 to, float speed)
    {
        return (to - from) * (speed * standSpeedMul);
    }
}
