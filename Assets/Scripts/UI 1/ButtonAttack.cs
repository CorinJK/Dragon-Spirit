using UnityEngine;

public class ButtonAttack : MonoBehaviour
{
    bool isClicked; //������ ������� ����������

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
