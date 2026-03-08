using System.Collections.Generic;

namespace SpaceBattle.Interfaces
{
    public interface IMoveCommandEndable
    {
        ICommand GetMoveCommand(); // Команда движения для остановки
        object GetUObject(); // Движущийся объект
        IQueue<ICommand> GetQueue(); // Очередь команд
    }
}