using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    public Animator animator;

    public float maxHealth = 75;
    float currentHealth;

    Dusman2AI enemy2ai;
    void Start()
    {
        currentHealth = maxHealth;
       enemy2ai=GetComponent<Dusman2AI>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0) {
            Die();
        }
    }
    void Die()
    {
        enemy2ai.leftrightspeed = 0;
        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
        enemy2ai.followspeed = 0;
        this.enabled = false;


    }

}
