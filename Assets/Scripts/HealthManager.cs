using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts;
    public GameManager gameManager;

    int hp;

    void Start()
    {
        ResetHealth();

        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();
    }

    public void ResetHealth()
    {
        hp = hearts.Length;
        Refresh();
    }

    public void TakeDamage(int amount = 1)
    {
        if (hp <= 0) return;

        hp -= amount;
        if (hp < 0) hp = 0;

        Refresh();

        if (hp == 0)
        {
            if (gameManager != null)
                gameManager.GameOver();
        }
    }

    void Refresh()
    {
        for (int i = 0; i < hearts.Length; i++)
            if (hearts[i] != null)
                hearts[i].enabled = (i < hp);
    }
}
