using UnityEngine;
using UnityEngine.UI;

public class SelectedArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;       //сюда в юнити перенести все фразы, напротив которых должна быть стрелка
    [SerializeField] private AudioClip changeSound;         //звук перемещения стрелки
    [SerializeField] private AudioClip interactSound;       //звук нажатия стрелкой
    private RectTransform rect;
    private int currentPosition;                //текущая позиция

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //само перемещение стрелки
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //взаимодействие с опциями
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void Interact()
    {
        SoundController.instance.PlaySound(interactSound);

        //получить доступ к каждой кнопке и вызвать функцию
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
            SoundController.instance.PlaySound(changeSound);

        //чтобы не выходило за пределы
        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        //назначение стрелке текущего Y параметра (перемещая вверх/вниз)
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }
}
