using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public Animator anim;
    public int maxHealt = 300;
    int currentHealt;
    DemonAI demonai;
    // Start is called before the first frame update
    void Start()
    {
        currentHealt = maxHealt;
        demonai = GetComponent<DemonAI>();
    }
    public void TakeDamage(int damage)
    {


        currentHealt -= damage;

        anim.SetTrigger("Hurt");

        if (currentHealt <= 0) 
        {
            Die();
        }
        //demonai.TakeDamage(damage); // DemonAI sýnýfýndaki TakeDamage fonksiyonunu çaðýr


    }
    void Die()
        {
            anim.SetBool("IsDead", true);
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject,2f);
        }

    }
