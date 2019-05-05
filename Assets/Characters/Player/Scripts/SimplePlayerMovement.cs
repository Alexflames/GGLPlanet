using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SimplePlayerMovement : NetworkBehaviour
{
    public float Speed = 25;
    readonly float _baseSpeedBySecond = 250;

    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    [Command]
    void CmdFlipSprite(bool flipX)
    {
        RpcFlipSprite(flipX);
    }

    [ClientRpc]
    void RpcFlipSprite(bool flipX)
    {
        _sprite.flipX = flipX;
    }

    void Move(Vector2 direction)
    {
        transform.Translate(direction * Speed / _baseSpeedBySecond);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            Vector2 direction = new Vector2();
            direction += new Vector2(Input.GetAxis("Horizontal"), 0);
            direction += new Vector2(0, Input.GetAxis("Vertical"));
            direction.Normalize();
            Move(direction);

            // Should sprite be mirrored by x?
            if (direction.x != 0)
            {
                bool isFlipNeeded = Mathf.Sign(direction.x) == 1 ? false : true;
                _sprite.flipX = isFlipNeeded;
                CmdFlipSprite(isFlipNeeded);
            }
        }
    }
}
