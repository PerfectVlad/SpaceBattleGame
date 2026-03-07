using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class BurnFuelCommand : ICommand
    {
        private readonly object target;

        public BurnFuelCommand(object target)
        {
            this.target = target;
        }

        public void Execute()
        {
            // Сжигание топлива
            Console.WriteLine("Сжигание топлива");
        }
    }
}