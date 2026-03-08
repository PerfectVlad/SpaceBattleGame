using SpaceBattle.Interfaces;

namespace SpaceBattle.Adapters
{
    public class VelocityChangeableAdapter : IVelocityChangeable
    {
        private (int x, int y)? currentVelocity;

        public void SetVelocity((int x, int y) velocity)
        {
            currentVelocity = velocity;
        }

        public void RemoveVelocity()
        {
            currentVelocity = null;
        }

        public (int x, int y)? GetVelocity()
        {
            return currentVelocity;
        }
    }
}