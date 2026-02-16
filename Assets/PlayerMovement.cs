using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;

    private Animator animator;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    private float jumpForce = 500f;
    private int groundMask;
    private bool isGrounded;
    private string currentAnimaton;
    private bool isAttackPressed;
    private bool isAttacking;
    private int attackType = 0;
    private Vector3 originalScale;

    [SerializeField]
    private float attackDelay = 0.3f;

    [SerializeField]
    private float punchDamage = 10f;

    [SerializeField]
    private float kickDamage = 15f;

    [SerializeField]
    private float attackRange = 1f;

    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private LayerMask enemyLayers;

    const string Player_Walk = "Walk";
    const string Player_Idle = "Idle";
    const string Player_Run = "Run";
    const string Player_Jump = "Jump";
    const string Player_left_punch = "Left_Punch";
    const string Player_Right_punch = "Right_Punch";
    const string Player_Kick = "Kick";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");

        originalScale = transform.localScale;

        if (rb2d == null) Debug.LogError("Rigidbody2D not found!");
        if (animator == null) Debug.LogError("Animator not found!");
        else Debug.Log("Animator found: " + animator.runtimeAnimatorController.name);
    }

    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            isAttackPressed = true;
            attackType = 0;
        }

 
        if (Input.GetMouseButtonDown(1))
        {
            isAttackPressed = true;
            attackType = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttackPressed = true;
            attackType = 2;
        }
    }

    private void FixedUpdate()
    {


        Vector2 vel = new Vector2(0, 0);

        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            vel.x = 0;
        }

        if (yAxis < 0)
        {
            vel.y = -walkSpeed;
        }
        else if (yAxis > 0)
        {
            vel.y = walkSpeed;
        }
        else
        {
            vel.y = 0;
        }

        if (!isAttacking)
        {
            if (xAxis != 0 || yAxis != 0)
            {
                ChangeAnimationState(Player_Run);
            }
            else
            {
                ChangeAnimationState(Player_Idle);
            }
        }

        rb2d.linearVelocity = vel;

        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;

                float damage = 0;

                if (attackType == 0)
                {
                    ChangeAnimationState(Player_left_punch);
                    damage = punchDamage;
                }
                else if (attackType == 1)
                {
                    ChangeAnimationState(Player_Right_punch);
                    damage = punchDamage;
                }
                else if (attackType == 2)
                {
                    ChangeAnimationState(Player_Kick);
                    damage = kickDamage;
                }

                DealDamageToEnemies(damage);

                Invoke(nameof(AttackComplete), attackDelay);
            }
        }
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void DealDamageToEnemies(float damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit enemy: " + enemy.name + " for " + damage + " damage!");

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation)
        {
            Debug.Log("Already playing: " + newAnimation + " - skipping");
            return;
        }

        Debug.Log("Changed animation from '" + currentAnimaton + "' to '" + newAnimation + "'");
        animator.CrossFade(newAnimation, 0f, 0);
        currentAnimaton = newAnimation;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
