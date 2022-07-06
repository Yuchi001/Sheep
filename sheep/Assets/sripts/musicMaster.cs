using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicMaster : MonoBehaviour
{
    public AudioSource main;
    private bool start = false;
    private void Start()
    {
        main.Play();
    }
}
