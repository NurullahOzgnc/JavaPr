using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject impactEffect;
    public float speed = 30f;
    public Rigidbody2D rb;
    public LayerMask enemyLayer;
    public float damageRange = 0.5f;
    private float damage = 25;
    enemy1 enemy1;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //col.GetComponent<enemy1>().TakeDamage(damage);
        col.GetComponent<enemy2>().TakeDamage(damage);
        //Damage buraya girilicek

        Destroy(gameObject);
        GameObject instantiate = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instantiate, 1f);
    }

    

}
