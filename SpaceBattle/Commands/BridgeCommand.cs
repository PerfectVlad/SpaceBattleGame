using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class BridgeCommand : ICommand
    {
        private ICommand internalCommand;

        public BridgeCommand(ICommand internalCommand)
        {
            this.internalCommand = internalCommand;
        }

        public void Inject(ICommand otherCommand)
        {
            internalCommand = otherCommand;
        }

        public void Execute()
        {
            internalCommand.Execute();
        }
    }
}