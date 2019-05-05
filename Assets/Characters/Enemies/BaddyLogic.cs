using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BaddyLogic : NetworkBehaviour
{
    public int HP = 12;

    public float Speed = 0.005f;

    public Vector2 moveDir;
    private float timeToNextMove = 0.1f;

    private float iFrames = 0;

    private bool isVulnurable = true;

    void Update()
    {
        if (!isServer) return;

        if (iFrames > 0)
        {
            iFrames -= Time.deltaTime;
            if (iFrames < 0)
            {
                isVulnurable = true;
                RpcVisualizeInvul(false);
            }
        }

        if (timeToNextMove <= 0)
        {
            moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            moveDir.Normalize();
            timeToNextMove = Random.Range(0.1f, 0.35f);
        }

        timeToNextMove -= Time.deltaTime;

        transform.Translate(moveDir * Speed);
    }

    public void TakeDamage()
    {
        if (!isVulnurable) return;
        HP--;
        iFrames = 0.5f;
        isVulnurable = false;
        if (HP == 0) NetworkServer.Destroy(gameObject);
        else RpcVisualizeInvul(true);
    }

    [ClientRpc]
    void RpcVisualizeInvul(bool invul)
    {
        float alpha = invul ? 0.5f : 1;
        var spriteColor = gameObject.GetComponent<SpriteRenderer>();
        spriteColor.color = new Color(spriteColor.color.r, spriteColor.color.g, spriteColor.color.b, alpha);
    }
}
