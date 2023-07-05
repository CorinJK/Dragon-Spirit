using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    //используем для определения расположение партикла
    public void SetDirection(float _direction)
    {
        gameObject.SetActive(true);     //проверка на активность

        //поворот направления
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)      //проверка на верную сторону 
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //деактивация партикла
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
