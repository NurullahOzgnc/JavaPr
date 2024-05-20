using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Knight_Combat : MonoBehaviour
{

    public Animator animator;
    public LayerMask enemyLayers;
    PlayerMovement playerMovement;
    private float runSpeed;
    private float currentHealth;
    private float maxHealth=200f;

    //AirAttack i�in
    public float airAttackRate = 2f;
    private float airNextAttackTime = 0f;
    public Transform airAttackPoint1;
    public Transform airAttackPoint2;
    public float airAttack2Range = 0.9f;

    //Attack Damagelar�
    private float normalAttackDamage = 25f;
    private float spinAttackDamage = 15f;
    private float burstAttackDamage = 50f;
    private float specialAttackDamage = 75f;



    //Attack 1 i�in
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Transform attackPoint;
    public float attackRange = 0.9f;


    //Attack 2 i�in
    public float attack2Rate = 10f;
    private float nextAttack2Time = 0f;
    public Transform attack2Point;
    public Transform attack2_2Point;
    public Transform attack2_1Point;
    public float attack2Range = 0.9f;
    public float attack2_1Range = 2f;

    //Attack 3 i�in
    public float attack3Rate = 2f;
    private float nextAttack3Time = 0f;
    public Transform attack3Point;
    public float attack3Range = 0.9f;

    //Special Attack i�in
    public float SpecialAttackRate = 2f;
    private float SpecialNextAttackTime = 0f;
    public Transform SpecialAttackPoint;
    public float SpecialAttackRange = 0.9f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        runSpeed = playerMovement.runSpeed;
        currentHealth = maxHealth;
        
    }


    void Update()
    {   
        
        //Attack cooldown i�in
        if (Time.time >= nextAttackTime && !playerMovement.jump)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                

            }
        }
        if (Time.time >= nextAttackTime && playerMovement.jump)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                AirAttack();
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
        animator.SetTrigger("Attack1");
        StartCoroutine(Attack1());
    }
    void AirAttack()
    {
        animator.SetTrigger("Attack1");
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

    void AirAttackAnimation()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(airAttackPoint1.position, airAttackPoint2.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(normalAttackDamage);
        }

    }
    void SpecialAttackOnAnimaton()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(SpecialAttackPoint.position, SpecialAttackRange, enemyLayers);
        foreach (var enemy in hitEnemies)
        {

            enemy.GetComponent<enemy1>().TakeDamage(specialAttackDamage);
        }
    }
    void Attack3OnAnimation()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack3Point.position, attack3Range, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(burstAttackDamage);
        }
    }




    void AttackOnAnimation()
    {
        //Belirlenen b�lgede belirlenen �apta daire olu�turur ve dairenin �arpt��� b�t�n nesneleri toplar
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack2Point.position, attack2Range, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            //Vurulan hasar buraya girilecek
            enemy.GetComponent<enemy3>().TakeDamage(normalAttackDamage);
            
        }
    }

    void Attack2OnAnimationNormal()
     {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Vurulan d��manlar� tutan listedeki herkese hasar uygulama
        foreach (var enemy in hitEnemies)
        {
            //Vurulan hasar buraya girilecek
            enemy.GetComponent<enemy1>().TakeDamage(spinAttackDamage);
        }
    }

    void Attack2OnAnimationSpin()
    {

     //Belirlenen b�lgede belirlenen �apta daire olu�turur ve dairenin �arpt��� b�t�n nesneleri toplar
         Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attack2_1Point.position, attack2_2Point.position, enemyLayers);

            //Vurulan d��manlar� tutan listedeki herkese hasar uygulama
         foreach (var enemy in hitEnemies)
         {
          //Vurulan hasar buraya girilecek
         }
     }


    IEnumerator Attack1()
    {
        
        playerMovement.movement = true;
        yield return new WaitForSeconds(0.8f);
        playerMovement.movement = false;
    }
    public void takeDamage(float damage)
    {
        
        currentHealth -= damage;
        StartCoroutine(WaitHurt());
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger("Dead");
        playerMovement.runSpeed = 0f;
        this.enabled = true;
        

    }
    IEnumerator WaitHurt()
    {
        animator.SetTrigger("Hurt");
        playerMovement.runSpeed = 0f;
        yield return new WaitForSeconds(0.4f);
        playerMovement.runSpeed = 25f;
    }
}
