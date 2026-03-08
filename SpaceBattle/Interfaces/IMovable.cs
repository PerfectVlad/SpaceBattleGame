using SpaceBattle.Exceptions;

namespace SpaceBattle.Interfaces
{
    public interface IMovable
    {
        // Текущая позиция объекта в пространстве
        /// <exception cref="UnableToGetPositionException">Если невозможно прочитать позицию</exception>
        (int x, int y) GetPosition();

        // Мгновенная скорость объекта
        /// <exception cref="UnableToGetVelocityException">Если невозможно прочитать скорость</exception>
        (int x, int y) GetVelocity();

        // Установить новую позицию объекта

        /// <param name="newPosition">Новая позиция (x, y)</param>
        /// <exception cref="UnableToSetPositionException">Если невозможно установить позицию</exception>
        void SetPosition((int x, int y) newPosition);
    }
}