using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletForce;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
        }

        Invoke("Disable", 2f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
