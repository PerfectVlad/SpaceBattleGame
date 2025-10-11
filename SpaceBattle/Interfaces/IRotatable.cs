using SpaceBattle.Exceptions;

namespace SpaceBattle.Interfaces
{
    public interface IRotatable
    {
        /// <summary>
        /// Текущий угол поворота объекта в градусах
        /// </summary>
        /// <returns>Угол в градусах (0-360)</returns>
        /// <exception cref="UnableToGetAngleException">Если невозможно прочитать угол</exception>
        int GetAngle();

        /// <summary>
        /// Угловая скорость объекта в градусах
        /// </summary>
        /// <returns>Угловая скорость в градусах</returns>
        /// <exception cref="UnableToGetAngularVelocityException">Если невозможно прочитать угловую скорость</exception>
        int GetAngularVelocity();

        /// <summary>
        /// Установить новый угол поворота объекта
        /// </summary>
        /// <param name="newAngle">Новый угол в градусах</param>
        /// <exception cref="UnableToSetAngleException">Если невозможно установить угол</exception>
        void SetAngle(int newAngle);
    }
}