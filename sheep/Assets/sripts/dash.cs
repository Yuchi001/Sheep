using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.12f);
    }
}
