using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Animator animator;
    public Transform firePoint;
    public Transform rainingArea;
    public GameObject normalArrowPrefab;
    public GameObject entangleArrowPrefab;
    public GameObject arrowShowerPrefab;
    public GameObject beamPrefab;
    public LayerMask enemyLayer;
    public Transform rainingArea1;
    public Transform rainingArea2;
    public Transform beamArea1;
    public Transform beamArea2;
    public Transform beamPoint;
    PlayerMovement playerMovement;

    //Attack Damagelerý
    private int normalArrowDamage = 25;
    private int entangleArrowDamage = 15;
    private int arrowShowerDamage = 50;
    private int beamDamage = 75;

    private float maxHealth = 100f;
    private float currentHealth;



    // Update is called once per frame
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = maxHealth;
    }   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Shoot");
            ShootNormalArrow();
            
        }
        
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Shoot");
            ShootEntangleArrow();
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("ArrowRain");
            ArrowRain();
            
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("SpecialAttack");
            SpecialAttack();
            
        }
        


    }

    void SpecialAttack()
    {
        StartCoroutine(SpecialAttackDelay());

    }

    IEnumerator SpecialAttackDelay()
    {
        yield return new WaitForSeconds(0.75f);
        GameObject beam = Instantiate(beamPrefab, beamPoint.position, beamPoint.rotation);
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(beamArea1.position, beamArea2.position, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(beamDamage);
            enemy.GetComponent<enemy2>().TakeDamage(beamDamage);
            Debug.Log(enemy);
        }

        Destroy(beam, 0.5f);
    }

    void ArrowRain()
    {
        StartCoroutine(ArrowShower());

    }

    IEnumerator ArrowShower()
    {
        yield return new WaitForSeconds(0.7f);
        GameObject arrowShower = Instantiate(arrowShowerPrefab, rainingArea.position, rainingArea.rotation);
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(rainingArea1.position,rainingArea2.position,enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<enemy1>().TakeDamage(arrowShowerDamage);
            enemy.GetComponent<enemy2>().TakeDamage(arrowShowerDamage);
            Debug.Log(enemy);
        }
        yield return new WaitForSeconds(1.07f);
        Destroy(arrowShower);
    }
    void ShootNormalArrow()
    {
        //Ateþ mantýðý
       StartCoroutine(NormalShootDelay());
    }
    IEnumerator NormalShootDelay()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(normalArrowPrefab, firePoint.position, firePoint.rotation);
    }

    void ShootEntangleArrow()
    {
        //Ateþ mantýðý
        StartCoroutine(NormalEntangleDelay());
    }
    IEnumerator NormalEntangleDelay()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(entangleArrowPrefab, firePoint.position, firePoint.rotation);
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
