using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public float throwStrength;
    public string rangedString;
    public GameObject projectile;
    public Transform arm;

    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.RangedWasPressed)
        {
            RangedAttack();
        }
    }

    void RangedAttack()
    {
        animator.SetTrigger(rangedString);

        Rigidbody2D rb = Instantiate(projectile, arm.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = transform.right.normalized * throwStrength;
        rb.transform.rotation = transform.rotation;
    }
}
