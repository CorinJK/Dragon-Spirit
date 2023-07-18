using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform destTransform;
    [SerializeField] private float alphaTime;
    [SerializeField] private float moveTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(AnimateTeleport(collision.gameObject));
    }

    private IEnumerator AnimateTeleport(GameObject target)
    {
        var sprite = target.GetComponent<SpriteRenderer>();
        Player playerMovement = target.GetComponent<Player>();

        //����� ����������
        playerMovement.canMove = false;
        yield return AlphaAnimation(sprite, 0);
        target.SetActive(false);

        yield return MoveAnimation(target);

        //����� �� ������������
        playerMovement.canMove = true;
        target.SetActive(true);
        yield return AlphaAnimation(sprite, 1);
    }

    //����������� ������
    private IEnumerator MoveAnimation(GameObject target)
    {
        var currentMoveTime = 0f;

        while (currentMoveTime < moveTime)
        {
            currentMoveTime += Time.deltaTime;

            //���� �����������: ������, ����, �������� �����������
            target.transform.position = Vector3.Lerp(target.transform.position, destTransform.position, currentMoveTime/moveTime);

            //��� ����
            yield return null;
        }
    }

    //��������� ������������
    private IEnumerator AlphaAnimation(SpriteRenderer sprite, float destAlpha)
    {
        var currentAlphaTime = 0f;
        var spriteAlpha = sprite.color.a;

        while (currentAlphaTime < alphaTime)
        {
            currentAlphaTime += Time.deltaTime;

            //������������� ����� � ������� �� 0 �� �����:
            var tmpAlpha = Mathf.Lerp(spriteAlpha, destAlpha, currentAlphaTime / alphaTime);
            var color = sprite.color;
            color.a = tmpAlpha;
            sprite.color = color;

            //��� ����
            yield return null;
        }
    }
}