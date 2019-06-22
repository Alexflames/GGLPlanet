using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclicMoveableObjects : MonoBehaviour
{
    [SerializeField]
    private Transform[] MoveableObjects;
    [SerializeField]
    private float MoveLimit = 30;
    [SerializeField]
    private float Speed = 3;
    [SerializeField]
    private Vector3 StandardDirection = Vector3.back;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var obj in MoveableObjects)
        {
            obj.Translate(StandardDirection * Time.deltaTime * Speed);
            if (Vector3.Distance(obj.position, startingPoint) > MoveLimit)
            {
                obj.position = startingPoint;
            }
        }
    }

    private Vector3 startingPoint;
}
