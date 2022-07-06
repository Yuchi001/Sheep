using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinCounter : MonoBehaviour
{
    private TextMeshProUGUI counter;
    void Start()
    {
        counter = gameObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        counter.text = PlayerPrefs.GetInt("coins", 0).ToString();
    }
}
