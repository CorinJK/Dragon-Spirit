using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    //���������� ��� ����������� ������������ ��������
    public void SetDirection(float _direction)
    {
        gameObject.SetActive(true);     //�������� �� ����������

        //������� �����������
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)      //�������� �� ������ ������� 
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //����������� ��������
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
