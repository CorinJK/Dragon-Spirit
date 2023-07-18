using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health heroHealth;         //здоровье игрока
    [SerializeField] private Image totalHealthBar;      //панель здоровья
    [SerializeField] private Image currentHealthBar;    //текущее изображение здоровья на панели

    //объем заполнения панели
    private void Start()
    {
        totalHealthBar.fillAmount = heroHealth.currentHealth / 5;
    }

    //расчет здоровья
    private void Update()
    {
        currentHealthBar.fillAmount = heroHealth.currentHealth / 5;
    }
}