using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage_amount, knockback_amount;
    [SerializeField] private Material mat_0, mat;
    [SerializeField] private float changeDuration = 1f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health;
        if (health = collision.GetComponent<Health>())
        {
            health.Get_Hit(damage_amount);
            StartCoroutine(ChangeMaterial(collision));
        }
    }







    private IEnumerator ChangeMaterial(Collider2D collision)
    {
        SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();

        sr.material = mat;

        yield return new WaitForSeconds(changeDuration);
        sr.material = mat_0;
    }

}
