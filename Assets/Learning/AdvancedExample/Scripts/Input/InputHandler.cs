using UnityEngine;

public class InputHandler {

    private readonly InputSystemActions inputSystemActions = new();

    public Vector2 MovementData { get; private set; }
    public Vector2 LookData { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsJumping { get; private set; }

    public void RegisterPlayerInput() {
        inputSystemActions.Player.Enable();

        inputSystemActions.Player.Move.performed += ctx => MovementData = ctx.ReadValue<Vector2>();
        inputSystemActions.Player.Move.canceled += ctx => MovementData = Vector2.zero;

        inputSystemActions.Player.Look.performed += ctx => LookData = ctx.ReadValue<Vector2>();
        inputSystemActions.Player.Look.canceled += ctx => LookData = Vector2.zero;

        inputSystemActions.Player.Sprint.performed += ctx => IsSprinting = ctx.ReadValueAsButton();
        inputSystemActions.Player.Sprint.canceled += ctx => IsSprinting = false;

        inputSystemActions.Player.Jump.performed += ctx => IsJumping = ctx.ReadValueAsButton();
        inputSystemActions.Player.Jump.canceled += ctx => IsJumping = false;
    }

    public void Dispose() {
        inputSystemActions.Player.Disable();
    }
}
