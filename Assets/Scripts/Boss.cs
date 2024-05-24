using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator anim;
    public float maxHealth = 100;
    float currentHealth;
    BossAI bossai;
    ArrowEntagle arrowEntaglee;
    
    void Start()
    {
        currentHealth = maxHealth;
        bossai = GetComponent<BossAI>();
        
    }

    public void TakeDamage(float damage)
    {
        
        
        currentHealth -= damage;
        anim.SetBool("Walking", false);     //damage aldýðý vaki yürüme anim. durduruyor
        anim.SetTrigger("Hurt");            //hasar alma anim. çalýþtýrýyor
        anim.SetBool("Walking", true);      //tekrardan yürümeye devam ediyor
        if (currentHealth <= 0)
        {
            Die();
        }
        void Die()
        {

            anim.SetBool("IsDead", true);
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            bossai.followSpeed = 0;
            Destroy(gameObject, 2f);


        }



    }

  
    
    }