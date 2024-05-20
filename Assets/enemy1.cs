using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy1 : MonoBehaviour
{
    public Animator animator;

    public float maxHealth = 100;
    public float currentHealth;
    public bool isDead = false;

    Dusman1AI enemy1ai;

    void Start()
    {
        currentHealth = maxHealth;
        enemy1ai = GetComponent<Dusman1AI>();
        
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemy1ai.speed = 0;
        this.enabled = false;
        isDead = true;
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        enemy1ai.speed = 0;
        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
    }
    void EnterHurt()
    {
        enemy1ai.speed = 0;
    }
    void ExitHurt()
    {
        enemy1ai.speed = 1f;
    }
}