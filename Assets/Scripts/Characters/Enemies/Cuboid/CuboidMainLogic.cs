using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CuboidAttackManager))]
public class CuboidMainLogic : NetworkBehaviour
{
    // current active movement controller
    // move switching is done by assigning reference to movement controller
    public MoveController currentMoveController;
    // random additive movement controller
    private ScaryCuboidMoveController randomWalking;
    // player-following movement controller
    private FollowMoveController followPlayerMovement;
    [SerializeField]
    private float Speed = 25f;

    [SerializeField]
    // the cube changes its movement style according to
    // the in-between attack timer, from random to follow and backwards
    private float changeMoveModeStartTimer = 5;
    // current time to move switch mode
    private float moveSwitchModeTimer;

    // cube attack manager 
    private CuboidAttackManager attackManager;

    // Start is called before the first frame update
    void Start()
    {
        randomWalking = new ScaryCuboidMoveController(gameObject, Speed, 0.1f);
        followPlayerMovement = new FollowMoveController(gameObject, Speed);
        currentMoveController = randomWalking;
        moveSwitchModeTimer = changeMoveModeStartTimer;

        attackManager = GetComponent<CuboidAttackManager>();
    }

    void FixedUpdate()
    {
        // Attacking
        attackManager.UpdateAttack();

        if (!isServer) return;

        // In-between attacks (movement logic)
        if (!attackManager.IsAttacking())
        {
            // update move only if no current attack is active
            currentMoveController.UpdateMove(Time.fixedDeltaTime);
            moveSwitchModeTimer -= Time.fixedDeltaTime;

            // time to switch movement modes?
            if (moveSwitchModeTimer < 0)
            {
                if (currentMoveController.GetType() == typeof(ScaryCuboidMoveController))
                {
                    currentMoveController = followPlayerMovement;
                    followPlayerMovement.SetTarget(GameObject.FindGameObjectsWithTag("Player")[0].transform);
                    // make follow mode 1.5 times longer than random, cause it is more scary and fun
                    moveSwitchModeTimer = changeMoveModeStartTimer * 1.5f;
                }
                else
                {
                    currentMoveController = randomWalking;
                    moveSwitchModeTimer = changeMoveModeStartTimer;
                }
            }
        }

        
    }
}
