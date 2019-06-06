using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCubeLinesAttack : BaseAttack
{

    [SerializeField]
    private float attackDuration = 2f;
    public override float duration {
        get {
            return attackDuration;
        }
    }

    public override int priority {
        get {
            return 1;
        }
    }

    public Color startingColor; // Starting color for damaging lines

    public GameObject linesContainer;
    private CollidingPlayers collidingPlayers;
    List<GameObject> damagedPlayers;
    public Material sourceMaterialToCopy;
    Material sharedLineMaterial;

    float rotationRNG;

    [SerializeField]
    private Color attColor = Color.black;
    public override Color attackColor {
        get {
            return attColor;
        }
    }
    
    private AttackManager mgr;

    // Start is called before the first frame update
    void Start()
    {
        sharedLineMaterial = new Material(sourceMaterialToCopy);
        var renderers = linesContainer.GetComponentsInChildren<SpriteRenderer>();
        foreach (Renderer rend in renderers)
        {
            rend.sharedMaterial = sharedLineMaterial;
        }
        collidingPlayers = linesContainer.GetComponent<CollidingPlayers>();
        mgr = gameObject.GetComponent<AttackManager>();
    }

    public override void AttStart()
    {
        rotationRNG = Random.Range(-90, 90);
        damagedPlayers = new List<GameObject>();
    }

    public override void AttUpdate(float attackTimeLeft)
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
                    if (!damagedPlayers.Contains(player))
                    {
                        damagedPlayers.Add(player);
                        mgr.InjurePlayer (player, 1);
                    }
                }
            }
        }
        else
        {
            // some action??
        }
    }

    public override void AttEnd()
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
        //AttEnd();
    }
}
