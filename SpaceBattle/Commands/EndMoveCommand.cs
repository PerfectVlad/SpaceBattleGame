using SpaceBattle.Interfaces;
using SpaceBattle.Adapters;

namespace SpaceBattle.Commands
{
    public class EndMoveCommand : ICommand
    {
        private readonly IMoveCommandEndable endable;

        public EndMoveCommand(IMoveCommandEndable endable)
        {
            this.endable = endable;
        }

        public void Execute()
        {
            //  Получаем команду движения
            var moveCommand = endable.GetMoveCommand();

            //  Если это BridgeCommand - инъектируем пустую команду
            if (moveCommand is BridgeCommand bridgeCommand)
            {
                var emptyCommand = new EmptyCommand();
                bridgeCommand.Inject(emptyCommand);
            }

            //  Удаляем скорость из объекта
            var uObject = endable.GetUObject();
            // Здесь будет логика удаления скорости из UObject
        }
    }

    public class EmptyCommand : ICommand
    {
        public void Execute()
        {
            // Пустая команда - ничего не делает
        }
    }
}