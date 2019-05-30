using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCubeLines : MonoBehaviour
{
    public float timeBetweenAttacks = 5f;
    private float TTA;

    public float attackDuration = 2f;
    private float attackTimeLeft;

    public Color startingColor;

    public GameObject linesContainer;
    private CollidingPlayers collidingPlayers;
    List<GameObject> damagedPlayers;
    public Material sourceMaterialToCopy;
    Material sharedLineMaterial;

    float rotationRNG;

    // Start is called before the first frame update
    void Start()
    {
        TTA = timeBetweenAttacks;

        sharedLineMaterial = new Material(sourceMaterialToCopy);
        var renderers = linesContainer.GetComponentsInChildren<SpriteRenderer>();
        foreach (Renderer rend in renderers)
        {
            rend.sharedMaterial = sharedLineMaterial;
        }
        collidingPlayers = linesContainer.GetComponent<CollidingPlayers>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TTA -= Time.fixedDeltaTime;
        if (TTA < 0)
        {
            AttStart();
            TTA = timeBetweenAttacks;
        }
        if (attackTimeLeft > 0)
        {
            AttUpdate();
            attackTimeLeft -= Time.fixedDeltaTime;
            if (attackTimeLeft <= 0)
            {
                AttEnd();
            }
        }

    }

    void AttStart()
    {
        attackTimeLeft = attackDuration;
        rotationRNG = Random.Range(-90, 90);
        damagedPlayers = new List<GameObject>();
    }

    void AttUpdate()
    {
        if (attackTimeLeft > attackDuration * 0.25)
        {
            sharedLineMaterial.color =
                new Color(
                    sharedLineMaterial.color.r,
                    sharedLineMaterial.color.g,
                    sharedLineMaterial.color.b,
                    Mathf.Lerp(1, 5, (attackDuration - attackTimeLeft) / (attackDuration - attackDuration * 0.25f))
                    );
            if (attackTimeLeft > attackDuration * 0.65)
            {
                linesContainer.transform.Rotate(
                    new Vector3(0, 0, rotationRNG * Time.fixedDeltaTime));
            }
            else if (attackTimeLeft <= attackDuration * 0.35)
            {
                var playersToHit = collidingPlayers.GetCollidingPlayers();
                
                foreach (GameObject player in playersToHit)
                {
                    AttackInformation attack = new AttackInformation(gameObject, 1);
                    if (!damagedPlayers.Contains(player))
                    {
                        damagedPlayers.Add(player);
                        player.GetComponentInParent<StatsManager>().DealDamage(attack);
                    }
                        
                }
            }
        }
        else
        {

        }
    }

    void AttEnd()
    {
        sharedLineMaterial.color =
                    new Color(
                        sharedLineMaterial.color.r,
                        sharedLineMaterial.color.g,
                        sharedLineMaterial.color.b,
                        1);
    }

    void OnDestroy()
    {
        AttEnd();
    }
}
