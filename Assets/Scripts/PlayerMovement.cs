using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public PlayerMovementStats moveStats;
    public float minY, maxY;

    private Rigidbody2D rigidbody;
    private Animator animator;

    //Movement Vars
    private Vector2 moveVelocity;
    private bool isFacingRight;

    // Start is called before the first frame update
    void Awake()
    {
        isFacingRight = true;

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (InputManager.RangedWasPressed)
        {
            animator.SetTrigger("isHurt");
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        //if(transform.position.y >= maxY)
        //{
        //    transform.position = new Vector3(transform.position.x,maxY,transform.position.z);
        //}
        //else if(transform.position.y <= minY)
        //{
        //    transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        //}

        Move(moveStats.GroundAcceleration, moveStats.GroundDeceleration, InputManager.Movement);
    }

    #region Movement

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            TurnCheck(moveInput);
            animator.SetBool("isWalking", true);

            Vector2 targetVelocity = Vector2.zero;
            targetVelocity = new Vector2(moveInput.x, moveInput.y) * moveStats.MaxWalkSpeed;

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
    #endregion
