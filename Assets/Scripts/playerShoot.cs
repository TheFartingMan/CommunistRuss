using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class playerShoot : MonoBehaviour

 

{
    public Vector2 targetPosition;
    public Camera cam;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float lifetime;

    public GameObject Target;
    public GameObject Aimer;
    private Target TargetScript;

    void Shoot()
    {
        

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Aimer.transform.rotation);
        bullet bulletScript = bullet.GetComponent<bullet>();
        bullet.transform.Rotate(0, 0, 90);
        bullet.GetComponent<bullet>().Initialize();
        bulletScript.AimerScript = Aimer.GetComponent<Aimer_Movement>();

        Destroy(bullet, lifetime);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Shoot();
        }
    }

    void Start()
    {
        TargetScript = Target.GetComponent<Target>();


        TargetScript.SpawnTarget();
        TargetScript.SpawnTarget();
        TargetScript.SpawnTarget();        
    }
}