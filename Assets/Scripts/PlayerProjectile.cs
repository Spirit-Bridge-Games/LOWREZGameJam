using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float lifetime = 2f;
    private float lifeCounter = 0;

    private void Awake()
    {
        lifeCounter = 0;  
    }

    private void Update()
    {
        lifeCounter += Time.deltaTime;
        if (lifeCounter >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Destroy(gameObject);
        }
    }
}
