using UnityEngine;

public class TubeMover : MonoBehaviour
{
    public float moveDistance = 250f; // cik tālu pa kreisi-pa labi (UI pikseļos)
    public float moveSpeed = 1.5f;    // ātrums

    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.localPosition = startLocalPos + new Vector3(x, 0f, 0f);
    }
}
