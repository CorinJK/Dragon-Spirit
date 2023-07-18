using UnityEngine;

public class Lava : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
            collision.GetComponent<Health>().TakeDamage(Mathf.Infinity);
    }
}