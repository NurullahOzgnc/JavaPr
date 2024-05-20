using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed = 30f;
    private float damage = 25;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
