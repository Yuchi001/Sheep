using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roboSheepCS : MonoBehaviour
{
    public GameObject blood;
    public GameObject boom;
    public GameObject[] bodyParts;
    public Transform[] bodyPartsPoints;
    public Transform[] rotations;
    public Animator anim;
    private int randomNum1;
    public float ms;
    public float attackRange;
    public float attackSpeed;
    private bool left = false;
    private SpriteRenderer sr;
    //sounds
    public GameObject boomS;
    //sounds
    private Vector3 playerPos;
    private Vector3 posDiff;
    private GameObject pos;
    // leftRightTool
    public Transform target;
    public float dirNum;
    private float leftInt;
    private bool fixedDir;
    // leftRightTool
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.flipX = true;
        pos = GameObject.FindGameObjectWithTag("Player");
        playerPos = pos.transform.position;
    }
    void Update()
    {
        switch (left)
        {
            case true:
                leftInt = -1;
                break;
            case false:
                leftInt = 1;
                break;
        }
        if(target!=null)
        {
            Vector3 heading = target.position - transform.position;
            dirNum = AngleDir(transform.forward, heading, transform.up);
            playerPos = pos.transform.position;
            posDiff = transform.position - playerPos;
            if (posDiff.x <= attackRange && dirNum == leftInt)
            {

            }

        }
        Debug.Log(dirNum);
    }
    public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
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
        fixedDir = !left;
        sr.flipX = fixedDir;
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
