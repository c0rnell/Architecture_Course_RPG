using System;

public interface IPlayerInput
{
    float Vertical { get; }
    float Horizontal { get; }
    float MouseX { get; }
    
    bool PausePressed { get; }
    event Action MoveModeTogglePressed;

    event Action<int> HotkeyPressed;
    void Tick();
}