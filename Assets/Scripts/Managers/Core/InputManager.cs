using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private InputActionAsset _inputActions;
    private InputAction _moveAction;
    private InputAction _confirmAction;
    private InputMode _inputMode = InputMode.Player;

    public InputActionMap PlayerMap { get; private set; }
    public InputActionMap UIMap { get; private set; }
    public Vector2 MoveInput => _inputMode == InputMode.Player ? _moveAction.ReadValue<Vector2>() : Vector2.zero;
    public bool ConfirmPressed => _inputMode == InputMode.UI && _confirmAction.WasPressedThisFrame();
    public bool GamePadConnected { get; private set; }

    public void Init()
    {
        _inputActions = InputSystem.actions;
        PlayerMap = _inputActions.FindActionMap("Player", true);
        UIMap = _inputActions.FindActionMap("UI", true);
        _moveAction = PlayerMap.FindAction("Move", true);
        _confirmAction = UIMap.FindAction("Submit", true);
        InputSystem.onActionChange += CheckDeviceType;
        SetInputMode(InputMode.Player);
    }

    public void SetInputMode(InputMode mode)
    {
        _inputMode = mode;
        PlayerMap.Disable();
        UIMap.Disable();

        if (mode == InputMode.Player)
            PlayerMap.Enable();
        else if (mode == InputMode.UI)
            UIMap.Enable();
    }

    public void Clear()
    {
        InputSystem.onActionChange -= CheckDeviceType;
        _inputActions.Disable();
    }

    private void CheckDeviceType(object actionObject, InputActionChange change)
    {
        if (change != InputActionChange.ActionPerformed)
            return;

        if (actionObject is InputAction action && action.actionMap?.asset == _inputActions)
            GamePadConnected = action.activeControl?.device is Gamepad;
    }
}
