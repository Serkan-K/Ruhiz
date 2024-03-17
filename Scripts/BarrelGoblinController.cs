using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class BarrelGoblinController : MonoBehaviour
{
    private Rigidbody2D goblinRB;
    private Animator goblinAnim;
    private GameObject mainChar;
    public GameObject explosion;
    private Vector2 goblinDir;
    public float timer = 5f;
    public float interval = 5f;

    public float speed = 4f;

    void Awake()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        goblinRB = gameObject.GetComponent<Rigidbody2D>();
        goblinAnim = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer>interval)
        {
            Invoke("timerRestart", 5f);
        }
        move();
        animations();
        closeDetect();
    }

    void move()
    {
        // Main char position minus enemy position. NOT NORMALİZED
        goblinDir = mainChar.transform.position - gameObject.transform.position;
        if (goblinDir.magnitude > 5f && timer > interval)
        {
            goblinRB.velocity = goblinDir.normalized * speed;
        }
        else
        {
            goblinRB.velocity = new Vector2(0, 0);
        }
    }

    void animations()
    {
        // Enemy rotation on x axis.
        if (goblinDir.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        // Activate running animation.
        goblinAnim.SetFloat("speed", goblinRB.velocity.magnitude);
    }

    void closeDetect()
    {
        if (goblinDir.magnitude < 3.0f)
        {
            goblinAnim.SetTrigger("fired");
            Invoke("destroyBarrel",0.62f);
        }
    }
    void destroyBarrel()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, quaternion.identity);
    }

    void timerRestart()
    {
        timer = 0;
        goblinRB.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Dynamite")
        {
            destroyBarrel();
        }
        // Ölüm animasyonu burada if komutu oluşturulacak eklenecek.
        // goblinAnim.SetTrigger("Death");
    }
}
