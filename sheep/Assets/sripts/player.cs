using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class player : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    public float dashSpeed;
    public float coolDown;
    public float jumpCoolDown;
    public float verticalJumpSpeed;
    public float horizontalJumpSpeed;
    public float shieldCoolDown;

    private bool ground = true;
    private bool right = true;
    private bool dash = true;
    private bool doubleJump=true;
    private bool shieldReady = true;
    private bool enable = false;
    private bool jump = true;
    private int randomNum1;
    public bool disable=false;

    public Animator anim;

    public Rigidbody2D rb2d;

    public ParticleSystem dustParticle;

    public GameObject trail;
    public GameObject dustAfterJump;
    public GameObject dashHitbox;
    public GameObject blood;
    public GameObject[] bodyParts;
    public GameObject bnqsieczepia;
    public GameObject shield;
    private GameObject clone;

    public Transform dustPoint;
    public Transform hitboxPoint;
    public Transform[] bodyPartsPoints;
    public Transform[] rotations;
    public Transform shieldPoint;

    public int lifes;
    //sounds
    public AudioSource healthUpSound;
    public AudioSource dashSound;
    public AudioSource shieldSound;
    public GameObject hurtSound;
    private AudioSource hS;
    public AudioSource jumpSound;
    public AudioSource coinSound;
    //sounds

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        PlayerPrefs.GetInt("coins", 0);
        PlayerPrefs.GetFloat("musicVolume", 0.3f);
        healthUpSound.volume= PlayerPrefs.GetFloat("musicVolume", 0.3f);
        dashSound.volume= PlayerPrefs.GetFloat("musicVolume", 0.3f);
        shieldSound.volume= PlayerPrefs.GetFloat("musicVolume", 0.3f);
        hS = hurtSound.GetComponent<AudioSource>();
        hS.volume = PlayerPrefs.GetFloat("musicVolume", 0.3f);
        jumpSound.volume= PlayerPrefs.GetFloat("musicVolume", 0.3f);
        coinSound.volume= PlayerPrefs.GetFloat("musicVolume", 0.3f); ;
    }
    void Update()
    {
        if(!disable || enable) rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        Debug.Log(ground);
        anim.SetFloat("velo", rb2d.velocity.y);
        anim.SetBool("ground", ground);
        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z)) && !disable && shieldReady)
        {
            shieldSound.Play();
            disable = true;
            shieldReady = false;
            enable = true;
            GameObject instance = Instantiate(shield, shieldPoint.position, transform.rotation);
            instance.transform.parent = gameObject.transform;
            Destroy(instance, 1f);
            movementSpeed /= 3;
            StartCoroutine(ShieldCoolDown());
            StartCoroutine(EnableTimeEnd(1f));
        }
        if ((Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Q)) && dash && !disable)
        {
            dashSound.Play();
            disable = true;
            dash = false;
            clone = Instantiate(dashHitbox, hitboxPoint.position, transform.rotation);
            clone.transform.parent = gameObject.transform;
            trail.SetActive(true);
            if (right) rb2d.velocity = new Vector2(rb2d.velocity.x + dashSpeed, rb2d.velocity.y);
            else rb2d.velocity = new Vector2(rb2d.velocity.x + dashSpeed * -1, rb2d.velocity.y);
            anim.SetBool("dashStart", true);
            StartCoroutine(DashCoolDown());
            StartCoroutine(SlowDown());
            StartCoroutine(DisableTimeEnd(0.3f));
        }
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && doubleJump && !disable)
        {
            jumpSound.Play();
            if (right) 
            {
                if(rb2d.velocity.y<0) 
                    rb2d.velocity = new Vector2(rb2d.velocity.x + horizontalJumpSpeed, rb2d.velocity.y + verticalJumpSpeed - rb2d.velocity.y);
                else if(rb2d.velocity.y>0) 
                    rb2d.velocity = new Vector2(rb2d.velocity.x + horizontalJumpSpeed, rb2d.velocity.y + verticalJumpSpeed - rb2d.velocity.y);
                else 
                    rb2d.velocity = new Vector2(rb2d.velocity.x + horizontalJumpSpeed, rb2d.velocity.y + verticalJumpSpeed);
            }
            else 
            {
                if (rb2d.velocity.y < 0)
                    rb2d.velocity = new Vector2(rb2d.velocity.x - horizontalJumpSpeed, rb2d.velocity.y + verticalJumpSpeed - rb2d.velocity.y);
                else if (rb2d.velocity.y > 0)
                    rb2d.velocity = new Vector2(rb2d.velocity.x - horizontalJumpSpeed, rb2d.velocity.y + verticalJumpSpeed - rb2d.velocity.y);
                else
                    rb2d.velocity = new Vector2(rb2d.velocity.x - horizontalJumpSpeed, rb2d.velocity.y + verticalJumpSpeed);
            }
            doubleJump = false;
            clone = Instantiate(bnqsieczepia, transform.position, transform.rotation);
            Destroy(clone, 1f);
            StartCoroutine(DoubleJumpCoolDown());
            StartCoroutine(DisableTimeEnd(0.3f));
        }
        if (!disable || enable)
        {
            if (!ground) rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            if (ground && !jump)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
                {
                    jumpSound.Play();
                    rb2d.velocity = new Vector2(0, jumpSpeed);
                    jump = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
            {
                if (rb2d.velocity.y != 0) ground = false;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (rb2d.velocity.y == 0) anim.SetBool("walk", true);
                if (!right)
                {
                    right = true;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                rb2d.velocity = new Vector2(movementSpeed, rb2d.velocity.y);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (rb2d.velocity.y == 0) anim.SetBool("walk", true);
                if (right)
                {
                    right = false;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                rb2d.velocity = new Vector2(-movementSpeed, rb2d.velocity.y);
            }
            else anim.SetBool("walk", false);
            
        }
        else anim.SetBool("walk", false);

    }
    IEnumerator ShieldCoolDown()
    {
        yield return new WaitForSeconds(shieldCoolDown);
        shieldReady = true;
    }
    IEnumerator DisableTimeEnd(float disableTime)
    {
        yield return new WaitForSeconds(disableTime);
        disable = false;
    }
    IEnumerator EnableTimeEnd(float disableTime)
    {
        yield return new WaitForSeconds(disableTime);
        disable = false;
        enable = false;
        movementSpeed *= 3;
    }
    IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(0.1f);
        rb2d.velocity = new Vector2(1, rb2d.velocity.y);
        anim.SetBool("dashStart", false);
        trail.SetActive(false);
        Destroy(clone, 0.1f);
        dash = false;
    }
    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        dash = true;
    }
    IEnumerator DoubleJumpCoolDown()
    {
        yield return new WaitForSeconds(jumpCoolDown);
        doubleJump = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("coin"))
        {
            coinSound.Play();
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0)+1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("healthUp"))
        {
            healthUpSound.Play();
            if(lifes<100)lifes++;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            ground = true;
            anim.SetBool("ground", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            ground = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            Instantiate(hurtSound, transform.position, transform.rotation);
            clone= Instantiate(blood, transform.position, transform.rotation);
            Destroy(clone, 3f);
            randomNum1 = Random.Range(0, 4);
            Instantiate(bodyParts[0], bodyPartsPoints[0].position, rotations[randomNum1].rotation);
            randomNum1 = Random.Range(0, 4);
            Instantiate(bodyParts[1], bodyPartsPoints[1].position, rotations[randomNum1].rotation);
            randomNum1 = Random.Range(0, 4);
            Instantiate(bodyParts[2], bodyPartsPoints[2].position, rotations[randomNum1].rotation);
            randomNum1 = Random.Range(0, 4);
            Instantiate(bodyParts[3], bodyPartsPoints[3].position, rotations[randomNum1].rotation);
            Destroy(gameObject);
            if (lifes != 0) lifes--;
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            anim.SetBool("ground", true);
            ground = true;
            jump = false;
            clone = Instantiate(dustAfterJump, dustPoint.position, transform.rotation);
            Destroy(clone, 1f);
        }
    }
}
