using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;


    public LayerMask enemyLayers;
    public LayerMask BossLayer;

    //Attack 1 için
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Transform attackPoint;
    public float attackRange = 0.9f;
    public int attackDamage = 25;


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
    }
    void Attack()
    {
        animator.SetTrigger("Attack1");

    }


    void AttackOnAnimation()
    {

        //Belirlenen bölgede belirlenen çapta daire oluþturur ve dairenin çarptýðý bütün nesneleri toplar
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitBosses = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, BossLayer);

        //Vurulan düþmanlarý tutan listedeki herkese hasar uygulama
        foreach (var enemy in hitEnemies)
        {
            //Vurulan hasar buraya girilecek
            if (enemy != null)
            {
                if (enemy.name == "Enemy1")
                {
                    enemy.GetComponent<enemy1>().TakeDamage(attackDamage);
                }
                else if (enemy.name == "Enemy2")
                {
                    enemy.GetComponent<enemy2>().TakeDamage(attackDamage);
                }
                else if (enemy.name == "Enemy3")
                {
                    enemy.GetComponent<enemy3>().TakeDamage(attackDamage);
                }
                else if (enemy.name == "Enemy4")
                {
                //   enemy.GetComponent<enemy4>().TakeDamage(attackDamage);
                }
              //  else if (enemy.name == "Boss")
              //  {
               //     enemy.GetComponent<Boss>().TakeDamage(attackDamage);
               // }
            }
            foreach (var boss in hitBosses)
            {
              if (boss != null)
                {
                    if (boss.name == "Boss")
                    {
                        boss.GetComponent<Boss>().TakeDamage(attackDamage);
                    }
                }
              //  boss.GetComponent<Boss>().TakeDamage(attackDamage);
            }
        }
    }





    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}