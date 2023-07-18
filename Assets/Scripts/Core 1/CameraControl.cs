using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float upDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    private float lookUp;

    //SmoothDamp для плавности передвижения камеры
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y + lookUp, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        lookUp = Mathf.Lerp(lookUp, upDistance, Time.deltaTime * cameraSpeed);
    }
}