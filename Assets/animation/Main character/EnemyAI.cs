using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    
    [SerializeField]
    private float chaseRange = 10f;
    
    [SerializeField]
    private float attackRange = 1.5f;
    
    [SerializeField]
    private float attackDamage = 5f;
    
    [SerializeField]
    private float attackCooldown = 1f;
    
    private Transform player;
    private Rigidbody2D rb2d;
    private float lastAttackTime;
    private bool canAttack = true;
    private Vector3 originalScale;
    
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure Player has the 'Player' tag!");
        }
        
        rb2d = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        lastAttackTime = -attackCooldown;
    }
    
    void Update()
    {
        if (player == null) return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= chaseRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                rb2d.linearVelocity = Vector2.zero;
                
                if (canAttack && Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            rb2d.linearVelocity = Vector2.zero;
        }
    }
    
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        
        rb2d.linearVelocity = direction * moveSpeed;
        
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
    
    void AttackPlayer()
    {
        lastAttackTime = Time.time;
        Debug.Log(gameObject.name + " attacked the player for " + attackDamage + " damage!");
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogWarning("Player doesn't have PlayerHealth script!");
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
