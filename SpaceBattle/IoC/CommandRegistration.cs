using SpaceBattle.Commands;
using SpaceBattle.Interfaces;

namespace SpaceBattle.IoC
{
    public static class CommandRegistration
    {
        public static void RegisterAll()
        {
            // Регистрируем базовые команды
            IoCContainer.Register("CheckFuel", args => 
                new CheckFuelCommand(args[0]));

            IoCContainer.Register("Move", args => 
                new MoveCommand((IMovable)args[0]));

            IoCContainer.Register("BurnFuel", args => 
                new BurnFuelCommand(args[0]));

            // Регистрируем макрокоманды
            MacroCommandStrategy.Register();
            LongRunningOperationStrategy.Register();
        }
    }
}