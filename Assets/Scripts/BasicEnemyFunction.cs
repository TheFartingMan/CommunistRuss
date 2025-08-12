using UnityEngine;

public abstract class BasicEnemyFunction : MonoBehaviour
{

    [SerializeField] public float EnemyHealth;
    [SerializeField] public float Speed;
    [SerializeField] public int Damage;
    [SerializeField] public string EnemyDebugNumber;
    public Transform target;

    protected virtual void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    protected virtual void Update()
    {
        Move(target.position);
        if (EnemyHealth <= 0)
        {
            Die();
        }
    }

    public virtual void TakeDamage(float amount)
    {
        EnemyHealth -= amount;
    }

    public virtual void Move(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
    }

    public abstract void Attack();

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            bullet bullet = other.GetComponent<bullet>();
            TakeDamage(bullet.damage);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            HealthBar healthBar = FindAnyObjectByType<HealthBar>();
            healthBar.SubtractHealth(Damage);
            Destroy(gameObject);
        }

    }



}
