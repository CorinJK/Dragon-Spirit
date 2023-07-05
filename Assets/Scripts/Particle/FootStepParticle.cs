using UnityEngine;

public class FootStepParticle : MonoBehaviour
{
    [SerializeField] private Transform spawnParticlePoint;  //�������, �� ������� ����� �������� ��������
    [SerializeField] private Player playerMovement;
    [SerializeField] private GameObject[] particle;         //������ ���������

    private void CreateParticle()
    {
        //��� ������ ����� � ������ ���� �� ������� �������� ��������� �� ������� �����
        particle[FindParticle()].transform.position = spawnParticlePoint.position;
        particle[FindParticle()].GetComponent<Particles>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    //�������� ��������
    private int FindParticle()
    {
        for (int i = 0; i < particle.Length; i++)
        {
            if (!particle[i].activeInHierarchy)     //�������� ������� �� �������
                return i;                           //���� ������ ������
        }
        return 0;
    }
}