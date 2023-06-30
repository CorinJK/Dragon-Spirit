using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    public void Interact()
    {
        action?.Invoke();
    }
}
