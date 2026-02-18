using UnityEngine;

public class HeadHitboxDamage : MonoBehaviour
{
    public HealthManager health;

    void Start()
    {
        if (health == null)
            health = FindFirstObjectByType<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Damage tikai no šiem tagiem
        if (other.CompareTag("Asteroid") || other.CompareTag("Weight"))
        {
            health.TakeDamage(1);

            // (opcija) iznīcināt objektu pēc trieciena, lai nav multi-hit
            Destroy(other.gameObject);
        }
    }
}
