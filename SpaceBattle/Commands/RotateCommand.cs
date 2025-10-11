using SpaceBattle.Interfaces;
using SpaceBattle.Exceptions;

namespace SpaceBattle.Commands
{
    public class RotateCommand
    {
        private readonly IRotatable rotatable;

        public RotateCommand(IRotatable rotatable)
        {
            this.rotatable = rotatable ?? throw new ArgumentNullException(nameof(rotatable));
        }

        public void Execute()
        {
            try
            {
                // Получаем текущий угол
                var angle = rotatable.GetAngle();
                
                // Получаем угловую скорость
                var angularVelocity = rotatable.GetAngularVelocity();
                
                // Вычисляем новый угол с учетом круговой системы (0-360 градусов)
                var newAngle = (angle + angularVelocity) % 360;
                
                // Если угол отрицательный, преобразуем в положительный
                if (newAngle < 0)
                {
                    newAngle += 360;
                }
                
                // Устанавливаем новый угол
                rotatable.SetAngle(newAngle);
            }
            catch (UnableToGetAngleException)
            {
                throw;
            }
            catch (UnableToGetAngularVelocityException)
            {
                throw;
            }
            catch (UnableToSetAngleException)
            {
                throw;
            }
        }
    }
}