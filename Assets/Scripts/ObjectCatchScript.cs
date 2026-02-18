using UnityEngine;

public class ObjectCatchScript : MonoBehaviour
{
    public float sizeIncrease = 0.5f;
    public float massIncrease = 1f;
    private Rigidbody2D rb;
    SFX_Script sfx;

    public VirtuluSkaititajs donutCounter; // <-- pieliec

    void Start()
    {
        sfx = FindFirstObjectByType<SFX_Script>();
        rb = GetComponent<Rigidbody2D>();

        if (donutCounter == null)
            donutCounter = FindFirstObjectByType<VirtuluSkaititajs>(); // auto atrod
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.IsChildOf(transform))
            return;

        if (collision.CompareTag("Donut"))
        {
            sfx.PlaySFX(4);

            // +1 donut
            if (donutCounter != null)
                donutCounter.AddDonut(1);

            Destroy(collision.gameObject);
            transform.localScale += new Vector3(sizeIncrease, sizeIncrease, 0);
            rb.mass += massIncrease;
        }
    }
}
