using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay references")]
    public DonutBakerScript baker;                 // Oven ar DonutBakerScript
    public MonoBehaviour mrBeanMovementScript;     // MrBean kustības skripts (CharacterControllerScript u.tml.)

    [Header("UI")]
    public GameObject startPoga;
    public GameObject retryPoga;

    [Header("Systems")]
    public HealthManager health;
    public VirtuluSkaititajs donutCounter;
    public VirtuluVertibuSkaititajs score;
    public Taimeris timer;

    [Header("Clean up tags on retry")]
    public string[] fallingObjectTags = { "Donut", "Asteroid", "Weight" };

    void Start()
    {
        // sākumā: nevar kustēties, nav retry pogas
        if (mrBeanMovementScript != null) mrBeanMovementScript.enabled = false;
        if (retryPoga != null) retryPoga.SetActive(false);
    }

    public void StartGame()
    {
        if (startPoga != null) startPoga.SetActive(false);
        if (retryPoga != null) retryPoga.SetActive(false);

        if (health != null) health.ResetHealth();
        if (donutCounter != null) donutCounter.ResetCount();
        if (score != null) score.ResetScore();
        if (timer != null) timer.StartTimer();

        if (mrBeanMovementScript != null) mrBeanMovementScript.enabled = true;

        if (baker != null) baker.BakeDonut(true);
    }

    public void GameOver()
    {
        // apstādina spēli
        if (baker != null) baker.BakeDonut(false);
        if (mrBeanMovementScript != null) mrBeanMovementScript.enabled = false;
        if (timer != null) timer.StopTimer();

        if (retryPoga != null) retryPoga.SetActive(true);
    }

    public void Retry()
    {
        // notīra jau krītošos objektus
        CleanupFallingObjects();

        // startē no nulles
        if (startPoga != null) startPoga.SetActive(false);
        StartGame();
    }

    void CleanupFallingObjects()
    {
        for (int i = 0; i < fallingObjectTags.Length; i++)
        {
            string t = fallingObjectTags[i];
            if (string.IsNullOrEmpty(t)) continue;

            // Unity nevar meklēt vairākus tagus vienā call, tāpēc cikls
            GameObject[] objs = GameObject.FindGameObjectsWithTag(t);
            for (int j = 0; j < objs.Length; j++)
                Destroy(objs[j]);
        }
    }
}
