using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(Event.current.keyCode);
    }
}
