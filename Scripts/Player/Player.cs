using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera main_Camera;
    [SerializeField] private InputActionReference aim_pos, attack_sword;
    [SerializeField] TrailRenderer dash_Trail;
    [SerializeField] private GameObject attack_area;

    private Rigidbody2D rb;
    private SpriteRenderer sprite_rd;
    private Inputs_ input_ = null;
    private Animator animator;





    [Header("Move Info")]
    public float moveSpeed = 8f;
    public float dash_Speed, dash_Duration, dash_Cooldown, attack_cooldown;

    private bool facingRight, can_Dash = true;
    private bool is_Dash, is_Attack = false;

    public bool isMove = true;

    public Vector2 move_Vector, aim_input;








    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite_rd = GetComponentInChildren<SpriteRenderer>();
        input_ = new Inputs_();
        animator = GetComponent<Animator>();

        facingRight = true;
        can_Dash = true;
    }


    private void OnEnable()
    {
        input_.Enable();
        input_.Player_inputs.Movement.performed += On_Movement_performed;
        input_.Player_inputs.Movement.canceled += On_Movement_cancelled;

        input_.Player_inputs.Attack.performed += OnAttack;
    }



    private void OnDisable()
    {
        input_.Disable();
        input_.Player_inputs.Movement.performed -= On_Movement_performed;
        input_.Player_inputs.Movement.canceled -= On_Movement_cancelled;

        input_.Player_inputs.Attack.performed -= OnAttack;
    }






    private void Update()
    {
        Face_direction();
        aim_input = Get_aim_input();
        animator.SetFloat("Move_speed", move_Vector.magnitude);

        if (is_Dash) { return; }
        //weaponParent.Aim_pos = aim_input;
    }

    private void FixedUpdate()
    {
        rb.velocity = move_Vector * moveSpeed;
        if (is_Dash) { return; }
    }














    private void On_Movement_performed(InputAction.CallbackContext value)
    {
        move_Vector = value.ReadValue<Vector2>();
    }
    private void On_Movement_cancelled(InputAction.CallbackContext value)
    {
        move_Vector = Vector2.zero;
    }


    public void On_Dash(InputAction.CallbackContext value)
    {
        if (value.performed && can_Dash)
        {
            StartCoroutine(DashCoroutine());
        }
    }




    private void OnAttack(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("attack");
            animator.SetTrigger("Attack");

            StartCoroutine(Attack_coroutine());
            
        }
    }

    private IEnumerator Attack_coroutine()
    {
        attack_area.SetActive(!is_Attack);
        yield return new WaitForSeconds(attack_cooldown);
        attack_area.SetActive(false);
    }

    private IEnumerator DashCoroutine()
    {
        can_Dash = false;
        is_Dash = true;

        dash_Trail.emitting = true;
        moveSpeed *= dash_Speed;

        yield return new WaitForSeconds(dash_Duration);

        moveSpeed /= dash_Speed;
        dash_Trail.emitting = false;
        is_Dash = false;

        yield return new WaitForSeconds(dash_Cooldown);
        can_Dash = true;

    }


    private void Face_direction()
    {
        Vector2 aimDirection = Get_aim_input() - (Vector2)transform.position;

        if (aimDirection.x < 0)
        {
            sprite_rd.flipX = true;
            facingRight = false;
        }
        else if (aimDirection.x > 0)
        {
            sprite_rd.flipX = false;
            facingRight = true;
        }
    }



    private Vector2 Get_aim_input()
    {
        Vector3 mouse_pos = aim_pos.action.ReadValue<Vector2>();
        mouse_pos.z = Camera.main.nearClipPlane;

        return Camera.main.ScreenToWorldPoint(mouse_pos);
    }

}
