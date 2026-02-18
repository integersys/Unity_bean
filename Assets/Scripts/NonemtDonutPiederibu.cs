using UnityEngine;

public class NonemtDonutPiederibu : MonoBehaviour
{
    [Tooltip("Kur pārparentot (piem., Canvas). Ja nav - atvienos uz Scene Root.")]
    public Transform reparentTo;

    [Header("Kurus tagus atvienot")]
    public string[] detachTags = { "Donut", "Asteroid", "Weight" };

    bool HasDetachTag(Transform t)
    {
        for (int i = 0; i < detachTags.Length; i++)
        {
            if (!string.IsNullOrEmpty(detachTags[i]) && t.CompareTag(detachTags[i]))
                return true;
        }
        return false;
    }

    void OnTransformChildrenChanged()
    {
        // atvieno jaunus bērnus zem Oven (un saglabā world pozīciju)
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            if (!HasDetachTag(child))
                continue;

            child.SetParent(reparentTo, true); // true = saglabā world pozīciju
        }
    }
}
