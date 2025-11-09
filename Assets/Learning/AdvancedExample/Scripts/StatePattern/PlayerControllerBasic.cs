using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterState {
    Idle,
    Walking,
    Running,
    Jumping
}

public class PlayerControllerBasic : MonoBehaviour {
    public CharacterController controller;

    private CharacterState state;
    private InputSystemActions inputSystem;

    private void Awake() {
        state = CharacterState.Idle;
    }

    public void ChangeState(CharacterState newState) {
        state = newState;
        Debug.Log($"Character is now: {state}");
        inputSystem = new InputSystemActions();
    }

    private void Update() {
        if(inputSystem.Player.Move.WasPerformedThisFrame()) {
            PerformAction(CharacterState.Walking);
        }
    }

    private void PerformAction(CharacterState newState) {
        switch(state) {
            case CharacterState.Idle:
                Debug.Log("Standing still...");
                break;
            case CharacterState.Walking:
                controller.Move(Vector3.forward * Time.deltaTime);
                Debug.Log("Walking forward...");
                break;
            case CharacterState.Running:
                controller.Move(Vector3.forward * 4f * Time.deltaTime);
                Debug.Log("Running fast...");
                break;
            case CharacterState.Jumping:
                controller.Move(Vector3.up);
                Debug.Log("Jumping high!");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
