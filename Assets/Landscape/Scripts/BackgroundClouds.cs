using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundClouds : MonoBehaviour
{
    [SerializeField]
    private GameObject[] clouds;
    [SerializeField]
    private float timeToSpawn = 2f;
    [SerializeField]
    private float timeLeft;

    [SerializeField]
    private float cloudSpeed = 2;
    private Vector3 cloudMovemementVector = new Vector3(1, 0 ,0);
    [SerializeField]
    private float cloudMovementLimit = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToSpawn;
    }

    void CreateCloud()
    {
        foreach (var cloud in clouds)
        {
            if (!cloud.activeSelf)
            {
                var pos = gameObject.transform.position;
                cloud.transform.position = new Vector3(pos.x, Random.Range(0, 12), pos.z);
                cloud.SetActive(true);
                break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime;
        foreach (var cloud in clouds)
        {
            if (cloud.activeSelf)
            {
                cloud.transform.Translate(cloudMovemementVector * Time.fixedDeltaTime * cloudSpeed);
                if (Vector3.Distance(cloud.transform.position, gameObject.transform.position) > cloudMovementLimit)
                {
                    print(Vector3.Distance(cloud.transform.position, gameObject.transform.position));
                    cloud.SetActive(false);
                }
            }
        }

        if (timeLeft < 0)
        {
            CreateCloud();
            timeLeft = Random.Range(timeToSpawn - (timeToSpawn / 4), timeToSpawn + (timeToSpawn / 4));
        }
    }
}
