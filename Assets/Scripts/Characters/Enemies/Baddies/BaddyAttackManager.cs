using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddyAttackManager : AttackManager
{
    [SerializeField]
    private float timeBetweenAttacks = 3f;
    private float TTA;

    private List<BaddyAttack> attacks = new List<BaddyAttack>();
    public BaddyAttack CurrentAttack { get; private set; }

    public void InjurePlayer(GameObject target, int damage)
    {
        AttackInformation attack = new AttackInformation(gameObject, damage);
        target.GetComponentInParent<StatsManager>().DealDamage(attack);
    }

    public override bool IsAttacking()
    {
        return CurrentAttack != null;
    }

    private void RegisterAttack(BaddyAttack a)
    {
        attacks.Add(a);
        
    }

    void Start()
    {
        TTA = timeBetweenAttacks;
        BaddyAttack[] foundAttacks = gameObject.GetComponents<BaddyAttack>();
        foreach (BaddyAttack a in foundAttacks)
        {
            RegisterAttack(a);
        }
    }

    protected override Attack ChooseAttack()
    {
        return attacks[Random.Range(0, attacks.Count)];
    }

    public override void UpdateAttack()
    {
        if (isLocalPlayer)
        {

        }

        if (!isServer) return;

        if (attacks.Count == 0) return;

        TTA -= Time.fixedDeltaTime;
        if (CurrentAttack == null)
        {
            if (TTA < 0)
            {
                CurrentAttack = ChooseAttack() as BaddyAttack;
                CurrentAttack.AttStart();
                TTA = CurrentAttack.duration;
            }
        }
        else
        {
            if (TTA > 0)
            {
                CurrentAttack.AttUpdate(TTA);
            }
            else
            {
                CurrentAttack.AttEnd();
                CurrentAttack = null;
                TTA = timeBetweenAttacks;
            }
        }
    }

}
