using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Hearts UI (left->right)")]
    public Image[] hearts; // 3 sirsniņas inspectorā (Heart1, Heart2, Heart3)

    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        RefreshHearts();
    }

    public void TakeDamage(int amount = 1)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        RefreshHearts();

        if (currentHealth == 0)
        {
            Debug.Log("Game Over!");
            // te vēlāk vari: stop movement, stop baking, show restart, utt.
        }
    }

    void RefreshHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
                hearts[i].enabled = (i < currentHealth);
        }
    }
}
