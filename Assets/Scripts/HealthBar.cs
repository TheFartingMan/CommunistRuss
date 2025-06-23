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

    private void Start()
    {
        currentHealth = totalHealth;
        totalhealthBar.fillAmount = currentHealth / totalHealth;
        Alive = true;
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

    public void SubtractHealth()
    {
        currentHealth--;
    }
}
