using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3 : MonoBehaviour
{
    public Animator animator;
    public float maxHealth = 50;
    float currentHealth;
    Dusman3AI enemy3ai;

    void Start()
    {
        currentHealth = maxHealth;
        enemy3ai = GetComponent<Dusman3AI>();
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
        enemy3ai.speed = 0;
        Debug.Log("Enemy Died!");
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
        this.enabled = false;
    }
}
