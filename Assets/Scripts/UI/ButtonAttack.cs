using UnityEngine;

public class ButtonAttack : MonoBehaviour
{
    bool isClicked; //вводим булевую переменную

    public void TaskOnClick()
    {
        isClicked = true;
    }

    void Update()
    {
        if (isClicked)
        {

        }
    }
}
