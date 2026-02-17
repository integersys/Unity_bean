using System.Collections;
using UnityEngine;

public class DonutBakerScript : MonoBehaviour
{
    public GameObject[] donutPrefabs;
    public float bakeInterval = 1.0f;
    float minPoz, maxPoz;
    Transform ovenTransform;
    public float offset = 0.7f;

    [Header("UI")]
    public GameObject startPoga;

    [Header("Start unlock")]
    public MonoBehaviour mrBeanMovementScript; // ievelc te MrBean kustības skriptu

    void Start()
    {
        ovenTransform = transform;

        // drošībai: lai tiešām sākumā nevar kustēt
        if (mrBeanMovementScript != null)
            mrBeanMovementScript.enabled = false;
    }

    public void BakeDonut(bool state)
    {
        if (state)
        {
            if (startPoga != null)
                startPoga.SetActive(false);

            if (mrBeanMovementScript != null)
                mrBeanMovementScript.enabled = true;

            StartCoroutine(Bake());
        }
        else
        {
            StopAllCoroutines();
            if (mrBeanMovementScript != null)
                mrBeanMovementScript.enabled = false;
        }
    }

    IEnumerator Bake()
    {
        while (true)
        {
            minPoz = ovenTransform.position.x - offset;
            maxPoz = ovenTransform.position.x + offset;
            float randPoz = Random.Range(minPoz, maxPoz);
            Vector2 spawnPoz = new Vector2(randPoz, ovenTransform.position.y);

            int donutIndex = Random.Range(0, donutPrefabs.Length);
            Instantiate(donutPrefabs[donutIndex], spawnPoz, Quaternion.identity, ovenTransform);
            yield return new WaitForSeconds(bakeInterval);
        }
    }
}
