using UnityEngine;

public class Damage : MonoBehaviour
{
    public HealthManager health;

    public SFX_Script sfx;          // <-- SFX skripts
    public int screamIndex = 5;    // <-- te ieliec index, kur ir scream.mp3

    void Start()
    {
        if (health == null)
            health = FindFirstObjectByType<HealthManager>();

        if (sfx == null)
            sfx = FindFirstObjectByType<SFX_Script>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid") || other.CompareTag("Weight"))
        {
            // Scream skaņa
            if (sfx != null)
                sfx.PlaySFX(screamIndex);

            if (health != null)
                health.TakeDamage(1);

            Destroy(other.gameObject);
        }
    }
}
