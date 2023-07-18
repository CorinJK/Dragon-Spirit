using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;     //сколько здоровья восстановит
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            SoundController.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().AddHealth(healthValue);    //увеличение здоровья
            gameObject.SetActive(false);                                //деактивация
        }
    }
}
