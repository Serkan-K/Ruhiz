using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int max_health;
    [SerializeField] private int current_health;

    private Animator animator;

    private void Awake()
    {
        current_health = max_health;
        animator = GetComponent<Animator>();
    }

    public void Get_Hit(int amount)
    {
        current_health -= amount;

        if(current_health <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(gameObject, 1);
        }
    }
}
