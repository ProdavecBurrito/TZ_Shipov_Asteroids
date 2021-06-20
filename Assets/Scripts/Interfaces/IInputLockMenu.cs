using System;

public interface IInputLockMenu
{
    public event Action<bool> OnMenuActive;
}