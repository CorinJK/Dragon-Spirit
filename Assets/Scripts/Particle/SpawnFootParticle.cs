using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject footPrefab;

    public void Spawn()
    {
        Instantiate(footPrefab, target.position, Quaternion.identity);
    }
}