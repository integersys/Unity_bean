using UnityEngine;

public class ObjectCatchScript : MonoBehaviour
{
    public float sizeIncrease = 0.5f;
    public float massIncrease = 1f;
    private Rigidbody2D rb;
    SFX_Script sfx;

    public VirtuluSkaititajs donutCounter;

    public VirtuluVertibuSkaititajs punktuSkaititajs; // <-- JAUNS

    void Start()
    {
        sfx = FindFirstObjectByType<SFX_Script>();
        rb = GetComponent<Rigidbody2D>();

        if (donutCounter == null)
            donutCounter = FindFirstObjectByType<VirtuluSkaititajs>();

        if (punktuSkaititajs == null) // <-- JAUNS
            punktuSkaititajs = FindFirstObjectByType<VirtuluVertibuSkaititajs>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.IsChildOf(transform))
            return;

        if (collision.CompareTag("Donut"))
        {
            sfx.PlaySFX(4);

            if (donutCounter != null)
                donutCounter.AddDonut(1);

            // --- JAUNS: pieskaita punktus atkarībā no donuta ---
            VirtuluVertiba value = collision.GetComponent<VirtuluVertiba>();
            int pts = (value != null) ? value.points : 0;

            if (punktuSkaititajs != null)
                punktuSkaititajs.AddPoints(pts);
            // ---------------------------------------------------

            Destroy(collision.gameObject);
            transform.localScale += new Vector3(sizeIncrease, sizeIncrease, 0);
            rb.mass += massIncrease;
        }
    }
}
