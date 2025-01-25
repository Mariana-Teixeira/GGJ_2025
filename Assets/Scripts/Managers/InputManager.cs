using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField] private UnityEvent OnKeyDown;
    
    public void CheckForKey(KeyCode key)
    {
        var isPressed = Input.GetKeyDown(key);
        if (isPressed) OnKeyDown.Invoke();
    }
}