using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Parent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer player_sp, weapon_sp;
    [SerializeField] private Transform circle_sword;

    public Animator animator_sword;
    public float circleRadius;
    public int Damage_;

    public Vector2 Aim_pos { get; set; }
    public bool Is_Attacking { get; private set; }


    public float delay_sword = .5f;
    private bool attack_Blocked;


    private void Update()
    {
        if (Is_Attacking) return;

        Vector2 direction = (Aim_pos - (Vector2)transform.position).normalized;
        transform.right = direction;


        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;



        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weapon_sp.sortingOrder = player_sp.sortingOrder - 1;
        }
        else
        {
            weapon_sp.sortingOrder = player_sp.sortingOrder + 1;
        }


    }




    public void Attack_sword()
    {
        if (attack_Blocked) return;

        animator_sword.SetTrigger("Attack_sword");
        Is_Attacking = true;
        attack_Blocked = true;

        StartCoroutine(Delay_attack());
    }

    private IEnumerator Delay_attack()
    {
        yield return new WaitForSeconds(delay_sword);
        attack_Blocked = false;
    }

    public void Reset_IsAttacking() { Is_Attacking = false; }


    public void Detect_Collider()
    {
        foreach (Collider2D collider
            in Physics2D.OverlapCircleAll(circle_sword.position, circleRadius))
        {
            //Health health_;
            //if (health_ = collider.GetComponent<Health>())
            //{
            //    health_.Get_Hit(Damage_, transform.parent.gameObject);
            //}

        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 c_pos = circle_sword == null ? Vector3.zero : circle_sword.position;
        Gizmos.DrawWireSphere(c_pos, circleRadius);
    }
}
