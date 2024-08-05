using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public float maxWalkSpeed;
    public float chaseDistance;
    public Collider2D attackCollider;

    private Rigidbody2D rigidbody;
    private Animator animator;

    //Movement Vars
    private Vector2 moveVelocity;
    private bool isFacingRight;

    //Attack vars
    public float attackDuration;
    private float attackTimer;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        isFacingRight = true;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackTimer = attackDuration;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) >= chaseDistance)
        {
            Move(5, 20, (playerTransform.position - transform.position).normalized);
            attackTimer = attackDuration;
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) < chaseDistance)
        {
            Move(5, 20, Vector2.zero);
            Attack();
        }
    }

    private void Attack()
    {
        if(attackTimer >= attackDuration)
        {
            attackCollider.enabled = true;
            animator.SetTrigger("isAttacking");
            attackTimer = 0;
        }
        else
        {
            attackCollider.enabled = false;
            attackTimer += Time.deltaTime;
        }
    }


    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            TurnCheck(moveInput);
            animator.SetBool("isWalking", true);

            Vector2 targetVelocity = Vector2.zero;
            targetVelocity = new Vector2(moveInput.x, moveInput.y).normalized * maxWalkSpeed;

            moveVelocity = Vector2.Lerp(moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            rigidbody.velocity = new Vector2(moveVelocity.x, moveVelocity.y);
        }
        else if (moveInput == Vector2.zero)
        {
            animator.SetBool("isWalking", false);

            moveVelocity = Vector2.Lerp(moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            rigidbody.velocity = new Vector2(moveVelocity.x, moveVelocity.y);
        }
    }

    private void TurnCheck(Vector2 moveInput)
    {
        if (isFacingRight && moveInput.x < 0)
            Turn(false);
        else if (!isFacingRight && moveInput.x > 0)
            Turn(true);
    }

    private void Turn(bool turnRight)
    {
        if (turnRight)
        {
            isFacingRight = true;
            transform.Rotate(0, 180f, 0);
        }
        else
        {
            isFacingRight = false;
            transform.Rotate(0, -180f, 0);
        }
    }
}
