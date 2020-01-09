using System;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
    public float Vertical => Input.GetAxis("Vertical");
    public float Horizontal => Input.GetAxis("Horizontal");

    public float MouseX => Input.GetAxis("Mouse X");
    
    public event Action<int> HotkeyPressed;

    public event Action MoveModeTogglePressed;

    public void Tick()
    {
        if (MoveModeTogglePressed != null && Input.GetKeyDown(KeyCode.Minus))
        {
            MoveModeTogglePressed.Invoke();
        }
        
        if (HotkeyPressed == null)
            return;
        
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                HotkeyPressed.Invoke(i);
            }
        }
    }
}