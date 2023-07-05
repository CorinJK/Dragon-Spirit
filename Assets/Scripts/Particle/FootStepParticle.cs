using UnityEngine;

public class FootStepParticle : MonoBehaviour
{
    [SerializeField] private Transform spawnParticlePoint;  //позиция, из которой будут выпущены партиклы
    [SerializeField] private Player playerMovement;
    [SerializeField] private GameObject[] particle;         //массив партиклов

    private void CreateParticle()
    {
        //при каждой атаке у одного шара из массива меняется положение на огневой точки
        particle[FindParticle()].transform.position = spawnParticlePoint.position;
        particle[FindParticle()].GetComponent<Particles>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    //проверка партикла
    private int FindParticle()
    {
        for (int i = 0; i < particle.Length; i++)
        {
            if (!particle[i].activeInHierarchy)     //проверка активен ли партикл
                return i;                           //тода вернем индекс
        }
        return 0;
    }
}