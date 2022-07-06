using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    private int randomNum1;
    public GameObject[] bodyParts;
    public Transform[] bodyPartsPoints;
    public Transform[] rotations;
    public GameObject sheep;
    private Rigidbody2D rb2d;
    private bool destroyable=false;
    public float velocityKill;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rb2d.velocity.y < velocityKill) destroyable = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dash"))
        {
            DestroyBox();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("wall")) && destroyable)
        {
            DestroyBox();
        }
    }
    private void DestroyBox()
    {
        Instantiate(sheep, transform.position, Quaternion.Euler(0, 0, 0));
        randomNum1 = Random.Range(0, 4);
        Instantiate(bodyParts[0], bodyPartsPoints[0].position, rotations[randomNum1].rotation);
        randomNum1 = Random.Range(0, 4);
        Instantiate(bodyParts[1], bodyPartsPoints[1].position, rotations[randomNum1].rotation);
        randomNum1 = Random.Range(0, 4);
        Instantiate(bodyParts[2], bodyPartsPoints[2].position, rotations[randomNum1].rotation);
        randomNum1 = Random.Range(0, 4);
        Instantiate(bodyParts[3], bodyPartsPoints[3].position, rotations[randomNum1].rotation);
        Destroy(gameObject);
    }
}
