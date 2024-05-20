using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Priestess_Combat : MonoBehaviour
{
    PlayerMovement playerMovement;
    public Animator animator;
    public LayerMask enemyLayers;
    private float normalAttackDamage = 20;
    private float burstAttackDamage = 45;
    private float specialAttackDamage = 37;
    private int healValue = 30;
    private float maxHealth = 125;
    private float currentHealth;



    //Attack 1 için
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Transform attackPoint1;
    public Transform attackPoint2;
    


    //Heal için
    public float healRate = 2f;
    private float nextHealTime = 0f;


    //Attack 3 için
    public float attack2Rate = 2f;
    private float nextAttack2Time = 0f;
    public Transform attack2_1Point;
    public Transform attack2_2Point;


    //Special Attack için
    public float SpecialAttackRate = 2f;
    private float SpecialNextAttackTime = 0f;
    public Transform SpecialAttackPoint1;
    public Transform SpecialAttackPoint2;



    private void Start()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        //Attack cooldown için
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                

            }
        }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Heal();
                nextHealTime = Time.time + 1f / nextHealTime;
                

        }

        if (Time.time >= nextAttack2Time)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack2();
                nextAttack2Time = Time.time + 1f / attack2Rate;
                

            }
         }
        if (Time.time >= SpecialNextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                SpecialAttack();
                SpecialNextAttackTime = Time.time + 1f / SpecialAttackRate;
                

            }
        }
    }
    void Attack()

    {
        animator.SetTrigger("Attack");

    }
    void Heal()
    {
        animator.SetTrigger("Heal");
        currentHealth += healValue;
    }
    void Attack2()
    {
        animator.SetTrigger("Attack2");
    }
    void SpecialAttack()
    {
        animator.SetTrigger("SpecialAttack");
    }


    void SpecialAttackOnAnimaton()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(SpecialAttackPoint1.position,SpecialAttackPoint2.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(specialAttackDamage);
        }
    }
    void Attack2OnAnimation()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attack2_1Point.position,attack2_2Point.position,enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            //Vurulan hasar buraya girilecek
            enemy.GetComponent<enemy1>().TakeDamage(burstAttackDamage);
        }
    }




    void AttackOnAnimation()
    {
        //Belirlenen bölgede belirlenen çapta daire oluþturur ve dairenin çarptýðý bütün nesneleri toplar
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPoint1.position, attackPoint2.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(normalAttackDamage);
        }
    }
    public void takeDamage(float damage)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger("Dead");
        playerMovement.runSpeed = 0f;

    }




    void OnDrawGizmosSelected()
        {
        Gizmos.DrawLine(SpecialAttackPoint1.position,SpecialAttackPoint2.position);
        Gizmos.DrawLine(attackPoint1.position,attackPoint2.position);
        Gizmos.DrawLine(attack2_1Point.position, attack2_2Point.position);
    }
    }

