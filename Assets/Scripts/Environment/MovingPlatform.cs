using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Edge Points")]
    [SerializeField] private Transform leftEdge;    //левый край
    [SerializeField] private Transform rightEdge;   //правый край

    [Header("Platform")]
    [SerializeField] private Transform platform;

    [Header("Movement parameters")]
    [SerializeField] private float speed;       //скорость
    private bool movingLeft;                    //движение влево

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;     //время сколько стоит у края
    private float idleTimer;                         //таймер сколько стоит у края

    private void Update()
    {
        if (movingLeft)
        {
            if (platform.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (platform.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    //направление движения
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        //движение по направлению
        platform.position = new Vector3(platform.position.x + Time.deltaTime * _direction * speed, platform.position.y, platform.position.z);
    }
}
