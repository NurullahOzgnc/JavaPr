using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusman2AI : MonoBehaviour
{

    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;
    public Transform firepoint;
    public float distance;
    private float attackRange = 1.5f;

    private Transform targetPos;
    public LayerMask playerLayer;
    public float followspeed;
    IsFlipped flip;

    private Animator animator;
    private bool canMove = true;
    void Start()
    {
        Physics2D.queriesStartInColliders = false;

        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//tag'i player olan nesneyi takip etmeyi saðlar.
        animator = GetComponent<Animator>();
        flip = GetComponent<IsFlipped>();
    }


    void Update()
    {
        if (canMove)
        {
            Flip();
            EnemyAi();
        }
    }
    void EnemyMove()
    {


        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * leftrightspeed, 1.0f));//lerp iki nokta arasý hesaplama sunuyor bizlere ayrýca 3.parametre bizim hýz hespalamasý yapmamýzý saðlýyor

        if (transform.position.x > oldPosition)//burada ise düþmanýn yüzünü nereye dönüp hareket edeceðini anlamamýzý saðlýyor
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (transform.position.x < oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;

    }
    void EnemyAi()//burada amaç ilk etapta eðer distance içinde ana karakteri görürse sanal çubuk kýrmýzý olacak ama eðer distance içinde karakteri görmezse yeþil olacak.Düþman algýlamasý için kullanýlýr.
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, -transform.right, distance);//sanal çubuðu çizmemize yarýyor ilk 2 parametre hangi yöne bakacaðýna karar verdirirken son parametre uzunluk içindir.

        if (hitEnemy.collider != null)//bir obje çarptý demek oluyor sanal çubuða.
        {
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
            FollowPlayer();
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(firepoint.position, attackRange, playerLayer);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position - transform.right * distance, Color.green);
            animator.SetBool("Attack", false);
            canMove = true;
            EnemyMove();
        }
    }
    void Flip()
    {
        if (animator.GetBool("Hurt") && !animator.GetBool("Attack"))
        {
            transform.Rotate(0f, 180f, 0f);
        }

    }
    void FollowPlayer()
    {

    }


    
}
