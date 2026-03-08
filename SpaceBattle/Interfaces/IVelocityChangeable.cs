namespace SpaceBattle.Interfaces
{
    public interface IVelocityChangeable
    {
        void SetVelocity((int x, int y) velocity);
        void RemoveVelocity();
        (int x, int y)? GetVelocity();
    }
}