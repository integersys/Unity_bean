using UnityEngine;
using TMPro;

public class VirtuluVertibuSkaititajs : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    int score = 0;

    void Start()
    {
        Refresh();
    }

    public void AddPoints(int amount)
    {
        score += amount;
        Refresh();
    }

    public void ResetScore()
    {
        score = 0;
        Refresh();
    }


    public int GetScore() => score;

    void Refresh()
    {
        if (scoreText != null)
            scoreText.text = $"Punkti: {score}";
    }
}
