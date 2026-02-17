using UnityEngine;

public class NonemtDonutPiederibu : MonoBehaviour
{
    [Tooltip("Ja ielikts, pārparentos uz šo (piem., Canvas). Ja nav - atvienos uz Scene Root.")]
    public Transform reparentTo;

    void OnTransformChildrenChanged()
    {
        // atvieno visus bērnus, kas parādījušies zem Oven
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            // ja gribi: tikai donut tag
            if (!child.CompareTag("Donut")) continue;

            // saglabā pasaules pozīciju
            child.SetParent(reparentTo, true); // reparentTo var būt null => aiziet uz root
        }
    }
}
