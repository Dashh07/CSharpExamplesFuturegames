using UnityEngine;

public class RunningState : ICharacterState {
    private readonly PlayerMovement playerMovement;
    private readonly InputHandler inputHandler;
    private readonly PlayerMovementSettings settings;

    public RunningState(PlayerMovement playerMovement, InputHandler inputHandler, PlayerMovementSettings settings) {
        this.playerMovement = playerMovement;
        this.inputHandler = inputHandler;
        this.settings = settings;
    }

    public void EnterState() {
        // update animations here if any
    }
    public void UpdateState() {
        playerMovement.MoveCharacter(settings.runSpeed);

        if (inputHandler.IsJumping && playerMovement.IsGrounded()) {
            playerMovement.SetState(new JumpingState(playerMovement, inputHandler, settings, this));
        }

        if (!inputHandler.IsSprinting) {
            playerMovement.SetState(new WalkingState(playerMovement, inputHandler, settings));
        }

        if (inputHandler.MovementData == Vector2.zero) {
            playerMovement.SetState(new IdleState(playerMovement, inputHandler, settings));
        }
    }
    public void ExitState() { }
}
