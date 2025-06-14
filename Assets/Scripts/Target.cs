using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject TargetPrefab;
    public Transform TargetPosition;
    [SerializeField] private Rigidbody2D rb;
    public Vector2 playerlocation;

    [SerializeField] private float speed;
    public Transform playerTransform;
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
        
    }

    public void SpawnTarget(){
        float randomPosx;
        float randomPosy;


        if (Random.value < 0.5f)
        {
            randomPosx = Random.Range(-9f, -8f);
        }
        else 
        {
            randomPosx = Random.Range(8f,9f);
        }
        randomPosy = Random.Range(-4f,4f);
        Vector2 Randomposition = new Vector2(randomPosx, randomPosy);
        GameObject Target = Instantiate(TargetPrefab, Randomposition, TargetPosition.rotation);
        Target.GetComponent<Collider2D>().enabled = true;
        Target.GetComponent<Target>().enabled = true;
    }

}
