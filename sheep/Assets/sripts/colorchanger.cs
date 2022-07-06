using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchanger : MonoBehaviour
{
    private SpriteRenderer sr;
    public float col = 0;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (col < 1) col += 0.001f;
        else col =0;
        sr.color = new Vector4(0, col, col, 0.2f);
    }
}
