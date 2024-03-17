using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PawnController : MonoBehaviour
{
    private Rigidbody2D goblinRB;
    private Animator goblinAnim;
    private AudioSource source;
    public AudioClip clip;
    private GameObject mainChar;
    private Vector2 goblinDir;
    private bool running = false;

    private float audioTimer = 2;
    public float audioInterval;
    public float speed = 10f;
    void Awake()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        goblinRB = gameObject.GetComponent<Rigidbody2D>();
        goblinAnim = gameObject.GetComponent<Animator>();
        source = gameObject.GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        move();
        animations();
        if (running == false)
        {
            audioTimer += Time.deltaTime;
        }
    }

    void move()
    {
        // Main char position minus enemy position. NOT NORMALİZED
        goblinDir = mainChar.transform.position - gameObject.transform.position;
        if (goblinDir.magnitude > 2f)
        {
            Invoke("Velocity", .5f);
        }
        else
        {
            goblinRB.velocity = new Vector2(0,0);
            running = false;
        }
    }

    void Velocity()
    {
        goblinRB.velocity = goblinDir.normalized * speed;
        running = true;
        if (audioTimer>audioInterval)
        {
            source.PlayOneShot(clip);
            audioTimer = 0f;
        }
    }
    void animations()
    {
        // Enemy rotation on x axis.
        if (goblinDir.x>0)
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
}