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
    private bool Alive;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Alive = true;
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
        if (other.CompareTag("Player") && Alive)
        {
            Alive = false;
            HealthBar = FindAnyObjectByType<HealthBar>();
            HealthBar.SubtractHealth();
            Destroy(gameObject);
        }
    }
    public void SpawnTarget()
    {
        float SharedRandomVariable = Random.value;
        float randomPosx;
        float randomPosy;
        randomPosx = 7* Mathf.Cos(SharedRandomVariable*2*Mathf.PI);
        randomPosy = 7* Mathf.Sin(SharedRandomVariable*2*Mathf.PI);

        Vector2 Randomposition = new Vector2(randomPosx, randomPosy);
        GameObject Target = Instantiate(TargetPrefab, Randomposition, TargetPosition.rotation);
        Target.GetComponent<Collider2D>().enabled = true;
        Target.GetComponent<Target>().enabled = true;
    }
    

}

