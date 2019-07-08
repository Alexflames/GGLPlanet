using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentBombarder : MonoBehaviour
{
    [SerializeField]
    private float timeToDrop = 1.9f;

    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private GameObject bombImpact;
    [SerializeField]
    private float bombDropTime = 2.0f;
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private float xDistance = 2.0f;
    [SerializeField]
    private float zDistance = 1.0f;

    [SerializeField]
    private GameObject shadow;

    void Start()
    {
        timeSinceStart = 0;
    }

    void FixedUpdate()
    {
        timeSinceStart += Time.fixedDeltaTime;
        if (timeSinceStart <= timeToDrop)
        {
            transform.Translate(new Vector3(0, 0, Time.fixedDeltaTime));
            shadow.transform.localScale += new Vector3(1, 1, 1) * Time.fixedDeltaTime;
        }
        else if (timeSinceStart >= timeToDrop && !exploded)
        {
            // Drop bomb
            bomb.SetActive(true);
            bomb.transform.Translate(bombDirection * Time.fixedDeltaTime);
            bombDirection.z = Mathf.Lerp
                (zDistance / (bombDropTime - distanceToGround), 0, 
                (timeSinceStart - timeToDrop) / bombDropTime) * speed;
            bombDirection.x = Mathf.Lerp
                (0, -xDistance / (bombDropTime - distanceToGround), 
                (timeSinceStart - timeToDrop) / bombDropTime) * speed;
        }

        if (timeSinceStart >= (timeToDrop + bombDropTime) && !exploded)
        {
            exploded = true;
            bombImpact.SetActive(true);
            bombImpact.transform.position = bomb.transform.position;
            bombImpact.transform.SetParent(null);
            bomb.SetActive(false);
            shadow.transform.SetParent(null);
        }

        if (exploded)
        {
            transform.Translate(new Vector3(0, 0, -Time.fixedDeltaTime));
            shadow.transform.localScale -= new Vector3(1, 1, 1) * Time.fixedDeltaTime;
            if (shadow.transform.localScale.x < 0)
            {
                Destroy(shadow);
                if (bombImpact != null)
                {
                    Destroy(bombImpact);
                }
                Destroy(gameObject);
            } 
        }
    }

    private float distanceToGround = 0.5f;
    private float timeSinceStart;
    // Need to move X- and Z-axis
    private Vector3 bombDirection = new Vector3(1, 0, 0);
    private bool exploded = false;
}
