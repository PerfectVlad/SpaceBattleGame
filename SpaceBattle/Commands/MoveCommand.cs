using SpaceBattle.Interfaces;
using SpaceBattle.Exceptions;

namespace SpaceBattle.Commands
{
    public class MoveCommand
    {
        private readonly IMovable movable;

        public MoveCommand(IMovable movable)
        {
            this.movable = movable ?? throw new ArgumentNullException(nameof(movable));
        }

        public void Execute()
        {
            try
            {
                // Получаем текущую позицию
                var position = movable.GetPosition();
                
                // Получаем скорость
                var velocity = movable.GetVelocity();
                
                // Вычисляем новую позицию
                var newPosition = (
                    x: position.x + velocity.x,
                    y: position.y + velocity.y
                );
                
                // Устанавливаем новую позицию
                movable.SetPosition(newPosition);
            }
            catch (UnableToGetPositionException)
            {
                throw;
            }
            catch (UnableToGetVelocityException)
            {
                throw;
            }
            catch (UnableToSetPositionException)
            {
                throw;
            }
        }
    }
}