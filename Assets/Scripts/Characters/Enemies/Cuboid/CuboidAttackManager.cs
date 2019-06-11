using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CuboidAttackManager : NetworkBehaviour
{
    [SerializeField]
    private float timeBetweenAttacks = 5f;
    private float TTA;

    [SerializeField]
    private Color baseColor = Color.white;
    [SerializeField]
    private GameObject[] gameObjectsToUseTransition;

    private List<CuboidAttack> attacks = new List<CuboidAttack> ();
    private CuboidAttack currentAttack;

    
    [SerializeField]
    // Should some attacks have more priority over others
    private bool priorityBasedChoosing;
    private int prioritiesSum = 0;
    private List<int> prioritiesCumulative = new List<int> ();

    private void RegisterAttack (CuboidAttack a) {
        attacks.Add (a);
        if (priorityBasedChoosing) {
            int pr = a.priority > 0 ? a.priority : 0;
            prioritiesCumulative.Add (prioritiesSum + pr);
            prioritiesSum += pr;
        }
    }

    public void InjurePlayer (GameObject target, int damage) {
        AttackInformation attack = new AttackInformation(gameObject, damage);
        target.GetComponentInParent<StatsManager>().DealDamage(attack);
    }

    void Start()
    {
        TTA = timeBetweenAttacks;
        CuboidAttack[] foundAttacks = gameObject.GetComponents<CuboidAttack> ();
        foreach (CuboidAttack a in foundAttacks) {
            RegisterAttack (a);
        }
    }

    private CuboidAttack ChooseAttack () {
        if (!priorityBasedChoosing) return attacks[Random.Range (0, attacks.Count)];
        int rndnum = Random.Range (0, prioritiesSum);
        int i;
        for (i = attacks.Count - 2; i>= 0; i--) {
            if (prioritiesCumulative[i] <= rndnum) return attacks[i + 1];
        }
        return attacks[0];
    }

    // Set color transition effect shader for boss and environment objects
    private void SetTransitionColorEffect(Color prev, Color next)
    {
        foreach (GameObject obj in gameObjectsToUseTransition)
        {
            if (obj == null) continue;
            var material = obj.GetComponent<Renderer>().material;
            material.SetColor("_ColorPrevious", prev);
            material.SetColor("_ColorNext", next);
            material.SetFloat("_TimeSinceTransitionStart", Time.time);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attacks.Count == 0) return;
        TTA -= Time.fixedDeltaTime;
        if (currentAttack == null) {
            if (TTA < 0)
            {
                currentAttack = ChooseAttack ();
                SetTransitionColorEffect(baseColor, currentAttack.attackColor);
                currentAttack.AttStart();
                TTA = currentAttack.duration;
            }
        }
        else {
            if (TTA > 0)
            {
                currentAttack.AttUpdate(TTA);
            } else {
                currentAttack.AttEnd();
                SetTransitionColorEffect(currentAttack.attackColor, baseColor);
                currentAttack = null;
                TTA = timeBetweenAttacks;
            }
        }
    }
}
