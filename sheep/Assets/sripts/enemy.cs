using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject blood;
    public GameObject boom;
    public GameObject[] bodyParts;
    public Transform[] bodyPartsPoints;
    public Transform[] rotations;
    public Animator anim;
    private int randomNum1;
    public float ms;
    public float jumpSpeed;
    private bool left = false;
    //sounds
    public GameObject boomS;
    //sounds
    void Update()
    {
        if (left) transform.rotation = Quaternion.Euler(0, 0, 0);
        if (!left) transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string inst = collision.gameObject.tag;
        switch (inst)
        {
            case "dash":

                Die();
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("path"))
        {
            transform.position = new Vector2(transform.position.x + ms * Time.deltaTime, transform.position.y);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("path"))
        {
            StartCoroutine(EndOfThePath());
        }
    }
    private void ChangeDirection()
    {
        left = !left;
        ms *= -1;
    }
    public void Die()
    {
        Instantiate(boomS);
        GameObject instance = Instantiate(boom, transform.position, transform.rotation);
        Destroy(instance, 0.8f);
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
    IEnumerator EndOfThePath()
    {
        anim.SetBool("idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("idle", false);
        ChangeDirection();
        if(left) transform.position = new Vector2(transform.position.x + (ms - 6f) * Time.deltaTime, transform.position.y);
        else transform.position = new Vector2(transform.position.x + (ms + 6f) * Time.deltaTime, transform.position.y);
    }
}
