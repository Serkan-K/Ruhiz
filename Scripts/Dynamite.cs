using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    private float speed = 1.5f;
    private GameObject player;
    private Rigidbody2D dynamiteRB;
    public GameObject explosionPrefab;
    void Start()
    {
        dynamiteRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 dynamiteDir = (player.transform.position + new Vector3(0,2,0)) - gameObject.transform.position;
        dynamiteRB.velocity = dynamiteDir * speed;
        Invoke("DestroyDynamite",2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Invoke("Rotation",0.1f);
    }

    void Rotation()
    {
        transform.Rotate(Vector3.back, - 10f);
    }

    void DestroyDynamite()
    {
        Destroy(gameObject);
        Instantiate(explosionPrefab,transform.position,quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Dynamite")
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab,transform.position,quaternion.identity);
        }
    }
}
