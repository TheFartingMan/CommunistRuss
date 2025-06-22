using UnityEngine;

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
    private float timer = 0f;
    private float fireCooldown;
    public int ShotsPerSecondMaximum;

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
        fireCooldown = 1f / ShotsPerSecondMaximum;
        timer += Time.deltaTime;

        bool fireHeld = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);

        if (fireHeld && timer >= fireCooldown)
        {
            Shoot();
            timer = 0f; // reset cooldown timer
        }
    }

    public void increaseShotsPerSecondMaximum()
    {
        ShotsPerSecondMaximum++;
    }
}