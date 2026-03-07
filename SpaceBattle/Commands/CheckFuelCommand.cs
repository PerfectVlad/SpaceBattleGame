using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class CheckFuelCommand : ICommand
    {
        private readonly object target;

        public CheckFuelCommand(object target)
        {
            this.target = target;
        }

        public void Execute()
        {
            // Проверка наличия топлива
            Console.WriteLine("Проверка топлива");
        }
    }
}