using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthbar;
    public float totalHealth;
    public float currentHealth;
    [SerializeField] private GameObject PlayerGameObject;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private AudioSource Musicsource;
    [SerializeField] private AudioSource DeathSoundEffectsource;
    [SerializeField] private AudioClip clip;
    private bool Alive;
    private bool Unkillable;
    private bool EveryOther;
    private float EnemyDamage;

    private void Start()
    {
        currentHealth = totalHealth;
        totalhealthBar.fillAmount = currentHealth / totalHealth;
        Alive = true;
        EveryOther = true;
        Unkillable = false;
    }

    private void Update()
    {
        currenthealthbar.fillAmount = currentHealth / totalHealth;
        if (currentHealth <= 0 && Alive == true)
        {
            PlayerGameObject.SetActive(false);
            GameOverScreen.SetActive(true);
            Musicsource.Stop();
            DeathSoundEffectsource.PlayOneShot(clip);
            Alive = false;

        }
    }

    public void SubtractHealth(int EnemyDamage)
    {
        if (Unkillable == false)
        {
            currentHealth -= EnemyDamage;   
        }
        
    }

    public void InfiniteHealth()
    {

        if (EveryOther == true)
        {
            Unkillable = true;
        }

        else
        {
            Unkillable = false;
        }
        
        EveryOther = !EveryOther;
    }
}
