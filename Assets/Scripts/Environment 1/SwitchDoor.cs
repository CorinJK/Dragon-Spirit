using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    [SerializeField] private Animator animDoor;
    [SerializeField] private Animator animSwitch;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            animSwitch.SetTrigger("interact");

            if (openTrigger)
            {
                animDoor.SetBool("isOpened", true);

                openTrigger = false;
                closeTrigger = true;
            }
            else if (closeTrigger)
            {
                animDoor.SetBool("isOpened", false);
                openTrigger = true;
                closeTrigger = false;
            }
        }
    }
}
