using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;  //массив врагов
    private Vector3[] initialPosition;

    private void Awake()
    {
        //проверка хватает ли позиций для всех врагов
        initialPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            //проверка не является ли поле пустым
            if (enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;
        }
    }

    //активация врагов в комнате
    public void ActivateRoom(bool _status)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
