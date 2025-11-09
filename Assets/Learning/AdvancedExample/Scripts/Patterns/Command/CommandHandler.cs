using System.Collections.Generic;
using UnityEngine;

public class CommandHandler {
    private bool shouldJump;
    private bool shouldUndo;

    public List<ICommand> commands = new();

    public CommandHandler() {
        CharacterController controller = new();
        Move move = new(controller);
        Jump jump = new(controller);

        ICommand command = null;

        if(shouldJump) {
            jump.Execute();
            commands.Add(jump);
            command = jump;
        }
        else {
            move.Execute();
            command = move;
        }

        if(shouldUndo) {
            if(commands.Count > 0) {
                commands.RemoveAt(commands.Count - 1);
            }
        }
    }
}
