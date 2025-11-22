using SpaceBattle.Interfaces;
using SpaceBattle.Exceptions;

namespace SpaceBattle.Commands
{
    public class MoveCommand : ICommand
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
                var position = movable.GetPosition();
                var velocity = movable.GetVelocity();
                
                var newPosition = (
                    x: position.x + velocity.x,
                    y: position.y + velocity.y
                );
                
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