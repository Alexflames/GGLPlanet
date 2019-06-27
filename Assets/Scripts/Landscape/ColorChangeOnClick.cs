using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnClick : MonoBehaviour
{
    [SerializeField]
    private Color colorTo = Color.white;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        colorFrom = material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            material.SetColor("_EmissionColor", material.GetColor("_EmissionColor") == colorTo * 8 ? colorFrom : colorTo * 8);
        }
    }
    
    private Color colorFrom;
    private Material material;
}
