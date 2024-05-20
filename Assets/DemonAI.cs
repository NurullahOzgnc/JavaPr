/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class DemonAI : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;
    private Transform target;
    public float followSpeed;

    private Animator anim;
    public float distance;
    private int currentHealth;
    private bool Attack=false;
    private bool Walk = false;


    void Start()
    {
        Physics2D.queriesStartInColliders = false;

        target = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        Player player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        DemonAi();
        DemonMove();
        DemonFollow();
        Die();
    }

    void DemonMove()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * leftrightspeed, 1.0f));
        
        if (transform.position.x > oldPosition)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("Walk", true);


        }
        if (transform.position.x < oldPosition) 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("Walk", true);

        }
        oldPosition =transform.position.x;

    }
    void DemonAi()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, -transform.right, distance);
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, -transform.right, distance,LayerMask.GetMask("Player"));

        if (hitEnemy.collider != null)
        {
            Debug.DrawLine(transform.position,hitEnemy.point, Color.red);
            anim.SetBool("Attack", true);
            DemonFollow();
        }
        else
        {
            Debug.DrawLine(transform.position,transform.position-transform.right*distance, Color.green);
            anim.SetBool("Attack", false);
            DemonMove();
        }
        if (hitPlayer.collider != null)
        {
            Debug.DrawLine(transform.position, hitPlayer.point, Color.red);
            anim.SetBool("demon.cleave", true); // Sald�r� animasyonunu ba�lat

            // Player'a hasar vermek i�in �a�r� yap
            Player player = hitPlayer.collider.GetComponent<Player>();
            if (player != null)
            {

            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position - transform.right * distance, Color.green);
            anim.SetBool("demon.cleave", false); // Sald�r� animasyonunu durdur
            DemonMove();
        }

    }

    void DemonFollow()
    {
        Vector3 targetPosition=new Vector3(target.position.x,gameObject.transform.position.y,target.position.x);
        transform.position=Vector2.MoveTowards(transform.position, targetPosition,followSpeed* Time.deltaTime);    
    }
    public void TakeDamage(int damage)
    {
        // Hasar miktar�n� g�ncelle
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("TakeHit");
        }

        // Hasar al�nd���nda ger�ekle�tirilecek i�lemler
        // �rne�in: Animasyon oynatma, �l�m kontrol�, vs.
    }
    void Die()
    {
        anim.SetBool("IsDead", true);
        Destroy(gameObject, 2f);
    }

}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class DemonAI : MonoBehaviour
{

    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;

    public float distance;

    private Transform target;
    public float followspeed;


    private Animator anim;


    void Start()
    {
        Physics2D.queriesStartInColliders = false;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        EnemyAi();
    }

    void EnemyMove()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * leftrightspeed, 1.0f));

        if (transform.position.x > oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);        }

        if (transform.position.x < oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;


    }

    void EnemyAi()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, -transform.right, distance);

        if (hitEnemy.collider != null)
        {
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
            anim.SetBool("Attack", true);
            EnemyFollow();


        }
        else
        {
            Debug.DrawLine(transform.position, transform.position - transform.right * distance, Color.green);
            anim.SetBool("Attack", false);
            EnemyMove();
        }



    }

    void EnemyFollow()
    {
        Vector3 targetPosition = new Vector3(target.position.x, gameObject.transform.position.y, target.position.x);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followspeed * Time.deltaTime);
    }





}   
*/


public class DemonAI : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float leftRightSpeed;
    public float distance;
    public float followSpeed;

    private Transform target;
    private Animator anim;
    private float oldPosition;
    private bool IsDead = false;
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!IsDead)
        demonAI();
    }
    /*
    private void DemonMove()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * leftRightSpeed, 1.0f));

        if (transform.position.x > oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        //oldPosition = transform.position.x;
    }
    */
    private void DemonMove()
    {
        // �ki nokta aras�nda lineer bir interpolasyon yaparak karakterin hareketini sa�la
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * leftRightSpeed, 1f));

        // E�er karakterin �u anki pozisyonu, �nceki pozisyonundan b�y�kse
        if (transform.position.x < oldPosition)
        {
            // Karakterin y�n�n� sa�a �evir
                     transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        // E�er karakterin �u anki pozisyonu, �nceki pozisyonundan k���kse
        else if (transform.position.x > oldPosition)
        {
            // Karakterin y�n�n� sola �evir
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        // Karakterin �u anki pozisyonunu oldPosition de�i�kenine ata
        oldPosition = transform.position.x;
    }

    private void demonAI()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, -transform.right, distance);

        if (hitEnemy.collider != null)
        {
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
            anim.SetBool("Attack", true);
            DemonFollow();
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position - transform.right * distance, Color.green);
            anim.SetBool("Attack", false);
            DemonMove();
        }
    }

    private void DemonFollow()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
