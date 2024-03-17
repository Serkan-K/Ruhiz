using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class TNTGoblinController : MonoBehaviour
{
    private Rigidbody2D goblinRB;
    private Animator goblinAnim;
    private GameObject mainChar;
    public GameObject dynamite;
    public AnimationClip AnimationClip;
    private Vector2 goblinDir;

    private bool escaping = false;
    public float speed = 3f;
    private float timer = 2f;
    private float interval = 2f;
    private float dynamitePos_x;
    private float dynamitePos_y = 2f;
    
    void Awake()
    {
        mainChar = GameObject.FindGameObjectWithTag("Player");
        goblinRB = gameObject.GetComponent<Rigidbody2D>();
        goblinAnim = gameObject.GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        move();
        animations(); 
        Attack();
        timer += Time.deltaTime;
    }
    
    void move()
    {
        // Main char position minus enemy position. NOT NORMALİZED
        goblinDir = mainChar.transform.position - gameObject.transform.position;
        if (goblinDir.magnitude > 6f)
        {
            goblinRB.velocity = goblinDir.normalized * speed;
            escaping = false;
        }
        else if (goblinDir.magnitude < 4f)
        {
            goblinRB.velocity = - goblinDir.normalized * speed;
            escaping = true;
        }
        else
        {
            goblinRB.velocity = new Vector2(0,0);
            escaping = false;
        }
    }
    
    void animations()
    {
        // Enemy orientation on x axis.
        if (goblinDir.x>0 && escaping == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            dynamitePos_x = 0.35f;
        }
        else if (goblinDir.x<0 && escaping == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            dynamitePos_x = -0.35f;
        }
        else if (goblinDir.x>0 && escaping == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            dynamitePos_x = -0.35f;
        }
        else if (goblinDir.x<0 && escaping == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            dynamitePos_x = 0.35f;
        }
        // Activate running animation.
        goblinAnim.SetFloat("speed", goblinRB.velocity.magnitude);
        
    }

    void Attack()
    {
        // Attack animation.
        if (goblinDir.magnitude < 6f && escaping == false)
        {
            Dynamite();
        }
    }

    public void Dynamite()
    {
        if (timer >= interval)
        {
            goblinAnim.SetTrigger("Attack");
            timer = 0f;
            Invoke("spawnDynamite",AnimationClip.length * 0.5f);
        }
    }

    public void spawnDynamite()
    {
        Instantiate(dynamite, 
            new Vector3(transform.position.x - dynamitePos_x,transform.position.y + dynamitePos_y,0), 
            quaternion.identity);
    }
}
