using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Hashashin_Combat : MonoBehaviour
{

    public Animator animator;
    public LayerMask enemyLayers;
    private float maxHealth = 125;
    private float currentHealth;
    PlayerMovement playerMovement;

    //Attack Damagelerý
    private float normalAttackDamage = 20;
    private float windAttackDamage = 23;
    private float tornadoAttackDamage = 50;
    private float specialAttackDamage = 25;




    //Attack 1 için
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Transform attackPoint1;
    public Transform attackPoint2;

    //Air attack için
    public Transform AirAttackPoint1;
    public Transform AirAttackPoint2;




    //Attack 2 için
    public float attack2Rate = 2f;
    private float nextAttack2Time = 0f;
    public Transform attack2_1Point;
    public Transform attack2_2Point;



    //Attack 3 için
    public float attack3Rate = 2f;
    private float nextAttack3Time = 0f;
    public Transform attack3_1Point;
    public Transform attack3_2Point;


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
        //    //Attack cooldown için
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                

            }
        }
        if (Time.time >= nextAttack2Time)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack2();
                nextAttack2Time = Time.time + 1f / attack2Rate;
                

            }
        }
        if (Time.time >= nextAttack3Time)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack3();
                nextAttack3Time = Time.time + 1f / attack3Rate;
                

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
    void Attack2()
    {
        animator.SetTrigger("Attack2");
    }
    void Attack3()
    {
        animator.SetTrigger("Attack3");
    }
    void SpecialAttack()
    {
        animator.SetTrigger("SpecialAttack");
    }


    void SpecialAttackOnAnimaton()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(SpecialAttackPoint1.position, SpecialAttackPoint2.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(specialAttackDamage);
            Debug.Log("Düþmana vuruldu");
        }
    }
    void Attack2OnAnimation()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attack2_1Point.position, attack2_2Point.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(windAttackDamage);
            //Vurulan hasar buraya girilecek
            Debug.Log("Düþmana vuruldu");
        }
    }

    void Attack3OnAnimation()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attack3_1Point.position, attack3_2Point.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(tornadoAttackDamage);
            Debug.Log("Düþmana vuruldu");
        }
    }

    void AttackOnAnimation()
    {
        //Ýki pozisyon arasý alan oluþuturu ve alanýn çarptýðý bütün nesneleri toplar
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPoint1.position, attackPoint2.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            //Vurulan hasar buraya girilecek
            Debug.Log("Düþmana vuruldu");
        }
    }

    void AirAttackOnAnimation()
    {
        //Ýki pozisyon arasý alan oluþuturu ve alanýn çarptýðý bütün nesneleri toplar
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(AirAttackPoint1.position, AirAttackPoint2.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            //Vurulan hasar buraya girilecek
            Debug.Log("Düþmana vuruldu");
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






}

