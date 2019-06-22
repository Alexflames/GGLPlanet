using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class with ready-to-use common next movement vectors
/// Move controller may use functions from this class
/// </summary>
public static class NPCMovement
{
    // Makes game-design movement values more adequate and user-friendly, 
    // like 20 insead of 1 and 75 instead of 3.75
    private static readonly float standSpeedMul = 0.05f;

    /// <summary>
    /// Purely random movement
    /// </summary>
    /// <param name="speed">20 equals to 1 unit per second</param>
    /// <returns>Next vector for Translate() with values based on 1 real-time second</returns>
    public static Vector2 ChaoticMovement(float speed)
    {
        var moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        moveDir.Normalize();
        return moveDir * (speed * standSpeedMul);
    }

    /// <summary>
    /// Random movement based on previous movement
    /// <para>It is recommended to calculate additive movement NOT every frame</para>
    /// </summary>
    /// <param name="oldMove">Previous movement (normalization is done automatically)</param>
    /// <param name="oldMoveContribution">Degree of contribution of old movement to the result;
    /// <para>Value of 0.5f is usually enough to make character sometimes move in a single direction</para>
    /// </param>
    /// <param name="speed">20 equals to 1 unit per second</param>
    /// <returns>Next vector for Translate() with values based on 1 real-time second</returns>
    public static Vector2 ChaoticMovementAdditive(Vector2 oldMove, float oldMoveContribution, float speed)
    {
        var moveDir = oldMove.normalized * oldMoveContribution +
            new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        moveDir.Normalize();
        return moveDir * (speed * standSpeedMul);
    }

    /// <summary>
    /// Returns directional movement vector multiplied by given and engine speed values
    /// </summary>
    /// <param name="dir">input directional movement</param>
    /// <param name="speed">20 equals to 1 unit per second</param>
    /// <returns>Next vector for Translate() with values based on 1 real-time second</returns>
    public static Vector2 DirectionalMovement(Vector2 dir, float speed)
    {
        return dir.normalized * (speed * standSpeedMul);
    }

    /// <summary>
    /// Calculates directional from-to movement vector multiplied by given and engine speed values
    /// </summary>
    /// <param name="from">position of movement start</param>
    /// <param name="to">position of movement end</param>
    /// <param name="speed">20 equals to 1 unit per second</param>
    /// <returns>Next vector for Translate() with values based on 1 real-time second</returns>
    public static Vector2 DirectionalMovement(Vector2 from, Vector2 to, float speed)
    {
        return (to - from).normalized * (speed * standSpeedMul);
    }
}
