﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StatsManager : NetworkBehaviour
{
    public int MaxHP = 5;
    [SyncVar(hook = nameof(HPChange))]
    protected int CurrentHP;

    // "Can entity take damage" properties
    public bool Invulnurable = false;
    // Invulnurabilty frames, while > 0 the entity is invulnurable
    public float invulTimeAfterHit = 0;
    float iFrames = 0; 
    private bool lastFrameVulnurable = true;

    public float Energy { get { return energy; } }

    public void DrainEnergy(float amount)
    {
        energy -= amount;
    } 

    private float energyMax = 100;
    private float energy;
    private float energyGainPerSecond = 5;

    void Start()
    {
        CurrentHP = MaxHP;
        energy = energyMax;
        StatsStart();
    }

    protected virtual void HPChange(int hp)
    {
        return;
    }

    protected virtual void StatsStart()
    {
        return;
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
        energy = Mathf.Min(energy + energyGainPerSecond * Time.fixedDeltaTime, energyMax);
    }

    bool isVulnurable()
    {
        bool vulnurable = true;
        vulnurable &= !Invulnurable;
        vulnurable &= iFrames <= 0;
        return vulnurable;
    }
    
    public void GiveHealth(int healthGiven)
    {
        if (!isServer) return;
        
        CurrentHP += healthGiven;
        HPChange(CurrentHP);
    }

    public void DealDamage(AttackInformation attack)
    {
        if (!isServer) return;

        if (isVulnurable())
        {
            CurrentHP -= attack.damage;
        }

        HitReact();
        HPChange(CurrentHP);

        if (CurrentHP < 0)
        {
            Dies();
        }
    }

    public void SpeedAddMul(float mul)
    {
        SimplePlayerMovement controller = gameObject.GetComponent<SimplePlayerMovement>();
        if (controller != null)
        {
            controller.speedMul *= mul;
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
