using SpaceBattle.Interfaces;
using SpaceBattle.Adapters;

namespace SpaceBattle.Commands
{
    public class StartMoveCommand : ICommand
    {
        private readonly IMoveCommandStartable startable;

        public StartMoveCommand(IMoveCommandStartable startable)
        {
            this.startable = startable;
        }

        public void Execute()
        {
            //  Получаем UObject и устанавливаем скорость
            var uObject = startable.GetUObject();
            var velocity = startable.GetVelocity();
            

            //  Создаем адаптер Movable
            var velocityChangeable = new VelocityChangeableAdapter();
            velocityChangeable.SetVelocity(velocity);
            var movableAdapter = new MovableAdapter(uObject, velocityChangeable);

            //  Создаем команду движения
            var moveCommand = new MoveCommand(movableAdapter);

            //  Создаем BridgeCommand и кладем в очередь
            var bridgeCommand = new BridgeCommand(moveCommand);
            var queue = startable.GetQueue();
            queue.Enqueue(bridgeCommand);
        }
    }
}