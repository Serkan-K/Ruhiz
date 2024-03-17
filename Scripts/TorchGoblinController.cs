using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class TorchGoblinController : MonoBehaviour
{
    private Rigidbody2D goblinRB;
    private Animator goblinAnim;
    private GameObject mainChar;
    private Vector2 goblinDir;

    public float speed = 3.5f;
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
        attack();
    }

    void move()
    {
        // Main char position minus enemy position. NOT NORMALİZED
        goblinDir = mainChar.transform.position - gameObject.transform.position;
        if (goblinDir.magnitude > 1.4f)
        {
            goblinRB.velocity = goblinDir.normalized * speed;
        }
        else
        {
            goblinRB.velocity = new Vector2(0,0);
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

    void attack()
    {
        // Attack animation.
        if (goblinDir.magnitude<1.8f)
        {
            goblinAnim.SetBool("Attack",true);
            
            // Attack Oriantation
            if (goblinDir.normalized.y > 0.707f)
            {
                goblinAnim.SetBool("Up", true);
            }else if (goblinDir.normalized.y < -0.707f)
            {
                goblinAnim.SetBool("Down", true);
            }
            else
            {
                goblinAnim.SetBool("Up", false);
                goblinAnim.SetBool("Down", false);
            }
        }else if (goblinDir.magnitude>=1.8f)
        {
            goblinAnim.SetBool("Attack",false);
        }
        
        
    }

    // Öldürüldükten sonraki animasyon.
    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    goblinAnim.SetTrigger("Death");
    //}
}
