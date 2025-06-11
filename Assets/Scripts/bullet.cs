using System;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    public GameObject Aimer;
    public Aimer_Movement AimerScript;
    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set the bullet's velocity in the direction it is facing
        rb.linearVelocity = transform.right * speed;
    }
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}