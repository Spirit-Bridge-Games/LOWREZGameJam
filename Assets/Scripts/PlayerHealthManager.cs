using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public FloatVariable health;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health.Value = 100;
    }

    public void Hit()
    {
        animator.SetTrigger("isHurt");
        GetComponent<Rigidbody2D>().AddForce(-transform.right, ForceMode2D.Impulse);
    }
}
