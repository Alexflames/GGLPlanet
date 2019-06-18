using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SimplePlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private float Speed = 25;
    private readonly float _baseSpeedBySecond = 250;

    private SpriteRenderer _sprite;
    [SerializeField]
    private List<SpriteRenderer> spritesToFlip = new List<SpriteRenderer>();

    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    [Command]
    void CmdFlipSprite(bool flipX)
    {
        RpcFlipSprite(flipX);
    }

    [ClientRpc]
    void RpcFlipSprite(bool flipX)
    {
        if (spritesToFlip.Count == 0)
        {
            _sprite.flipX = flipX;
        }
        else
        {
            foreach (var sprite in spritesToFlip)
            {
                sprite.flipX = flipX;
            }
        }
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
                CmdFlipSprite(isFlipNeeded);
            }

            if (Input.GetButton("Rotate Camera Left")) {
                gameObject.transform.Rotate(new Vector3(0, 0, 3));
            }
            if (Input.GetButton("Rotate Camera Right"))
            {
                gameObject.transform.Rotate(new Vector3(0, 0, -3));
            }
        }
    }
}
