using SpaceBattle.Interfaces;
using SpaceBattle.Exceptions;

namespace SpaceBattle.Adapters
{
    public class MovableAdapter : IMovable
    {
        private readonly object uObject;
        private readonly IVelocityChangeable velocityChangeable;

        public MovableAdapter(object uObject, IVelocityChangeable velocityChangeable)
        {
            this.uObject = uObject;
            this.velocityChangeable = velocityChangeable;
        }

        public (int x, int y) GetPosition()
        {
            // Здесь будет логика получения позиции из UObject
            return (0, 0);
        }

        public (int x, int y) GetVelocity()
        {
            var velocity = velocityChangeable.GetVelocity();
            if (velocity == null)
                throw new UnableToGetVelocityException();
            return velocity.Value;
        }

        public void SetPosition((int x, int y) newPosition)
        {
            // Здесь будет логика установки позиции в UObject
        }
    }
}