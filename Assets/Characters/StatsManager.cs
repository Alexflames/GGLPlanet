using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StatsManager : NetworkBehaviour
{
    public int StartHP = 5;
    [SyncVar]
    private int CurrentHP;

    // "Can entity take damage" properties
    public bool Invulnurable = false;
    // Invulnurabilty frames, while > 0 the entity is invulnurable
    public float invulTimeAfterHit = 0;
    float iFrames = 0; 
    private bool lastFrameVulnurable = true;

    void Start()
    {
        CurrentHP = StartHP;
    }

    void FixedUpdate()
    {
        // "Can entity take damage" block
        if (iFrames > 0)
        {
            iFrames -= Time.deltaTime;
        }
        // If entity changed its vulnurability
        if (isVulnurable() != lastFrameVulnurable)
        {
            lastFrameVulnurable = !lastFrameVulnurable;
            VisualizeInvul(lastFrameVulnurable);
        }
        // Another block
    }

    bool isVulnurable()
    {
        bool vulnurable = true;
        vulnurable &= !Invulnurable;
        vulnurable &= iFrames <= 0;
        return vulnurable;
    }

    public void DealDamage(AttackInformation attack)
    {
        if (!isServer) return;

        if (isVulnurable())
        {
            CurrentHP -= attack.damage;
        }

        HitReact();

        if (CurrentHP < 0)
        {
            Dies();
        }
    }
    
    // Make object transparent if cannot be hit
    void VisualizeInvul(bool vulnurable)
    {
        float alpha = !vulnurable ? 0.5f : 1;
        var spriteColor = gameObject.GetComponent<SpriteRenderer>();
        spriteColor.color = new Color(spriteColor.color.r, spriteColor.color.g, spriteColor.color.b, alpha);
    }

    protected virtual void Dies()
    {
        NetworkServer.Destroy(gameObject);
    }

    protected virtual void HitReact()
    {
        iFrames = invulTimeAfterHit;
    }
}
