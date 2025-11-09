using UnityEngine;

public interface ICommand {
    public void Execute();
    public void Undo();
}

public class Move : ICommand {
    private Vector3 prevPosition;
    private CharacterController controller;
    public Move(CharacterController controller) {
        this.controller = controller;
    }
    public void Execute() {
        prevPosition = controller.transform.position;
        controller.Move(Vector3.forward);
    }
    public void Undo() {
        controller.Move(prevPosition);
    }
}

public class Jump : ICommand {
    private Vector3 prevPosition;
    private CharacterController controller;

    public Jump(CharacterController controller) {
        this.controller = controller;
    }

    public void Execute() {
        prevPosition = controller.transform.position;
        controller.Move(Vector3.up);
    }
    public void Undo() {
        controller.Move(prevPosition);
    }
}

public class Attack : ICommand {

    public Attack() {
        // Provide necessary data
    }

    public void Execute() {
       Debug.Log("Attack");
    }
    public void Undo() {
        Debug.Log("Undo Attack");
    }
}




