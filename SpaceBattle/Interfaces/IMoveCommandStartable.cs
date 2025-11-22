using System.Collections.Generic;

namespace SpaceBattle.Interfaces
{
    public interface IMoveCommandStartable
    {
        object GetUObject(); // Возвращает объект для движения
        (int x, int y) GetVelocity(); // Возвращает скорость
        IQueue<ICommand> GetQueue(); // Возвращает очередь команд
    }
}