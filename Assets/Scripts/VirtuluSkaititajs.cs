using UnityEngine;
using TMPro;

public class VirtuluSkaititajs : MonoBehaviour
{
    public TextMeshProUGUI countText;

    int count = 0;

    void Start()
    {
        Refresh();
    }

    public void AddDonut(int amount = 1)
    {
        count += amount;
        Refresh();
    }

    public int GetCount() => count;

    void Refresh()
    {
        if (countText != null)
            countText.text = $"Savāktie virtuļi: {count}";
    }
}
