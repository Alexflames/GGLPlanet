using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboidAttackManager : MonoBehaviour
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

    public void RegisterAttack (CuboidAttack a) {
        attacks.Add (a);
    }

    public void InjurePlayer (GameObject target, int damage) {
        AttackInformation attack = new AttackInformation(gameObject, 1);
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

    public CuboidAttack ChooseAttack () {
        return attacks[Random.Range (0, attacks.Count)];
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
                foreach (GameObject obj in gameObjectsToUseTransition)
                {
                    var material = obj.GetComponent<Renderer>().material;
                    material.SetColor("_ColorPrevious", baseColor);
                    material.SetColor("_ColorNext", currentAttack.attackColor);
                    material.SetFloat("_TimeSinceTransitionStart", Time.time);
                }
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
                foreach (GameObject obj in gameObjectsToUseTransition)
                {
                    var material = obj.GetComponent<Renderer>().material;
                    material.SetColor("_ColorPrevious", currentAttack.attackColor);
                    material.SetColor("_ColorNext", baseColor);
                    material.SetFloat("_TimeSinceTransitionStart", Time.time);
                }
                currentAttack = null;
                TTA = timeBetweenAttacks;
            }
        }

    }
}
