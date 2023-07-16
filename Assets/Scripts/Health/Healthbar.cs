using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health heroHealth;         //�������� ������
    [SerializeField] private Image totalHealthBar;      //������ ��������
    [SerializeField] private Image currentHealthBar;    //������� ����������� �������� �� ������

    //����� ���������� ������
    private void Start()
    {
        totalHealthBar.fillAmount = heroHealth.currentHealth / 5;
    }

    //������ ��������
    private void Update()
    {
        currentHealthBar.fillAmount = heroHealth.currentHealth / 5;
    }
}