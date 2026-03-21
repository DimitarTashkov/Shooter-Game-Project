namespace Shooter_Game0._1.Core.Contracts
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
