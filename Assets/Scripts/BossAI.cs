using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;
    public float distance;
    private Transform target;
    public float followSpeed;
    private float originalFollowSpeed;
    public LayerMask playerLayer;
    public Transform attackPoint;
    private float attackRange = 2f;

    private Animator anim;
    public float damageToPlayer = 10;
    Vector3 demonPos;



    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        oldPosition = transform.position.x;
        originalFollowSpeed = followSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        bossAi();
        demonPos = transform.position- new Vector3(0,2f,0);

    }
    void bossMove()
    {

        // Oyuncuyu takip etmesi

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        FlipSpriteBasedOnDirection();


    }
    private void bossAi()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(demonPos, -transform.right, distance);

        if (hitEnemy.collider != null)
        {
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
            anim.SetBool("Attack", true);

            BossFollow();
            StartCoroutine(StopForAttack());

        }
        else
        {

            Debug.DrawLine(transform.position, transform.position - transform.right * distance, Color.green);
            anim.SetBool("Attack", false);
            bossMove();
        }
    }

    void BossFollow()
    {

       
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
    private void FlipSpriteBasedOnDirection()
    {

        // Boss'un y�n�n� de�i�tirmek i�in
        if (transform.position.x > oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;
    }
    IEnumerator StopForAttack()
    {
        // Sald�r� animasyonu s�resince bekle
        followSpeed = 0;        // Takip h�z�n� durdur
        anim.SetBool("Walking", false);
        yield return new WaitForSeconds(1f); // Sald�r� s�resini ayarla

        // Oyuncunun hala sald�r� menzilinde olup olmad���n� kontrol et
        RaycastHit2D hitEnemy = Physics2D.Raycast(demonPos, -transform.right, distance, playerLayer);
        if (hitEnemy.collider != null)
        {
            // E�er oyuncu hala menzildeyse sald�r�ya devam et
            StartCoroutine(StopForAttack());
        }
        else
        {
            // Oyuncu menzilden ��kt�ysa normale d�n
            anim.SetBool("Attack", false);
            anim.SetBool("Walking", true);
            followSpeed = originalFollowSpeed;
        }



    }

    void AttackOnAnimation()
    {

        //Belirlenen b�lgede belirlenen �apta daire olu�turur ve dairenin �arpt��� b�t�n nesneleri toplar
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,playerLayer);

        //Vurulan d��manlar� tutan listedeki herkese hasar uygulama
        foreach (var player in hitPlayer)
        {
            //Vurulan hasar buraya girilecek
            player.GetComponent<Bow>().takeDamage(damageToPlayer);
        }
    }

}
 

