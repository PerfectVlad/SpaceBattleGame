using SpaceBattle.Exceptions;

namespace SpaceBattle.Interfaces
{
    public interface IMovable
    {
        /// <summary>
        /// Текущая позиция объекта в пространстве
        /// </summary>
        /// <returns>Пара координат (x, y)</returns>
        /// <exception cref="UnableToGetPositionException">Если невозможно прочитать позицию</exception>
        (int x, int y) GetPosition();

        /// <summary>
        /// Мгновенная скорость объекта
        /// </summary>
        /// <returns>Вектор скорости (vx, vy)</returns>
        /// <exception cref="UnableToGetVelocityException">Если невозможно прочитать скорость</exception>
        (int x, int y) GetVelocity();

        /// <summary>
        /// Установить новую позицию объекта
        /// </summary>
        /// <param name="newPosition">Новая позиция (x, y)</param>
        /// <exception cref="UnableToSetPositionException">Если невозможно установить позицию</exception>
        void SetPosition((int x, int y) newPosition);
    }
}