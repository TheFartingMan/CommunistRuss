using UnityEngine;
using System.Collections;
public class Target : MonoBehaviour
{
    public GameObject TargetPrefab;
    public Transform TargetPosition;
    [SerializeField] private Rigidbody2D rb;
    public Vector2 playerlocation;

    [SerializeField] private float speed;
    public Transform playerTransform;
    private HealthBar HealthBar;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MoveTarget();
    }

    public void MoveTarget()
    {
        playerlocation = new Vector2(playerTransform.position.x, playerTransform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerlocation, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {

            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            HealthBar = FindAnyObjectByType<HealthBar>();
            HealthBar.SubtractHealth(1);
            Destroy(gameObject);
        }
    }
    

}

