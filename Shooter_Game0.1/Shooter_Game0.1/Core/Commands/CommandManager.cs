using Shooter_Game0._1.Core.Contracts;

namespace Shooter_Game0._1.Core.Commands
{
    public class CommandManager
    {
        private Stack<ICommand> commandHistory;

        public CommandManager()
        {
            commandHistory = new Stack<ICommand>();
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandHistory.Push(command);
        }

        public bool UndoPreviousCommand()
        {
            if (commandHistory.Count > 0)
            {
                ICommand command = commandHistory.Pop();
                command.Undo();
                return true;
            }
            return false;
        }

        public bool HasHistory => commandHistory.Count > 0;
    }
}