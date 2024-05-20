using System.Collections;
using UnityEngine;

public class Dusman3AI : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Animator animator;
    private Transform playerPos;
    private bool playerFound = false;
    private Transform currentPosition;
    public float speed = 1f;
    private Rigidbody2D rb;
    private float lookDistance = 13f;
    private bool followPlayer = false;
    private bool isFacingRight = false;
    private float flipValue;
    private float attackRange = 8f;
    private float attackCooldown = 4f; // Ýki saldýrý arasý bekleme süresi
    private float timeSinceLastAttack = 0f; // Son saldýrýnýn üzerinden geçen zaman
    enemy1 enemy;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public PlayerMovement playerMovement;
    public GameObject arrowPrefab;


    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerPos = playerMovement.transform;
        currentPosition = pointB.transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FoundPlayer();
        if (!playerFound)
        {
            Patrol();
        }
        if (followPlayer)
        {
            FollowPlayer();
            FlipIfNeeded();
            AttackIfNeeded();
        }
        

        timeSinceLastAttack += Time.deltaTime;

    }

    void Patrol()
    {
        Vector2 point = currentPosition.position - transform.position;
        rb.velocity = point.normalized * speed;
        if (Vector2.Distance(transform.position, currentPosition.position) < 0.5f)
        {
            flip();
            currentPosition = (currentPosition == pointA.transform) ? pointB.transform : pointA.transform;
        }
    }

    void FoundPlayer()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, playerPos.position - transform.position, lookDistance);

        if (hitPlayer.collider != null && hitPlayer.collider.CompareTag("Player"))
        {
            playerFound = true;
            followPlayer = true;
        }
    }

    void FollowPlayer()
    {
        Vector2 target = new Vector2(playerPos.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        flipValue = target.x - newPos.x;
    }

    void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isFacingRight = !isFacingRight;
    }

    void FlipIfNeeded()
    {
        if (flipValue < 0 && isFacingRight)
        {
            flip();
        }
        else if (flipValue > 0 && !isFacingRight)
        {
            flip();
        }
    }
    void AttackIfNeeded()
    {
        if (timeSinceLastAttack >= attackCooldown)
        {
            if (Vector2.Distance(playerPos.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(stopForAttack());
                timeSinceLastAttack = 0f; // Saldýrý yapýldýðýnda zamanlayýcýyý sýfýrla
            }
            else
            {
                animator.ResetTrigger("Attack");

            }
        }
    }
    IEnumerator stopForAttack()
    {
        speed = 0f;
        yield return new WaitForSeconds(0.5f);
        speed = 1f;
    }
    void AttackAnimation()
    {
        Instantiate(arrowPrefab, attackPoint.position, attackPoint.rotation);
    }
}
