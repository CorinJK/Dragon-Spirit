using UnityEngine;

public class ScoreParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem scoreParticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
            scoreParticle.Play();
    }
}
