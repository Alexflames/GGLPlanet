using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearRotatingObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis = new Vector3(1, 1, 1);
    [SerializeField]
    private float speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis * (Time.deltaTime * speed));
    }
}
