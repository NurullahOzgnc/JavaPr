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
        anim.SetBool("Walking", false);     //damage ald��� vaki y�r�me anim. durduruyor
        anim.SetTrigger("Hurt");            //hasar alma anim. �al��t�r�yor
        anim.SetBool("Walking", true);      //tekrardan y�r�meye devam ediyor
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