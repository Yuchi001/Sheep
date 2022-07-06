using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyParts : MonoBehaviour
{
    public GameObject blood;
    private Rigidbody2D rb2d;
    public Vector2 force;
    private Vector2 realForce;
    //sounds
    public GameObject boomS;
    private AudioSource sound;
    void Start()
    {
        force.x = 3f;
        force.y = 3f;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        realForce.x = Random.Range(-force.x, force.x);
        realForce.y = Random.Range(0.1f, force.y);
        rb2d.AddForce(realForce, ForceMode2D.Impulse);
        StartCoroutine(DestroyBody());
    }
    private void Update()
    {
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }
    IEnumerator DestroyBody()
    { 
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        GameObject x =Instantiate(boomS);
        sound = x.gameObject.GetComponent<AudioSource>();
        sound.volume= PlayerPrefs.GetFloat("musicVolume", 0.3f);
        GameObject instance = Instantiate(blood, transform.position, transform.rotation);
        Destroy(instance, 3f);
        Destroy(x, 2f);
        Destroy(gameObject);
    }
}
